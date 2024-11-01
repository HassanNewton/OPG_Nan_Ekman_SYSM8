using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data; // För ICollectionView

namespace FitTrack.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
    {
        // Refererar till Usermanager klassen
        private readonly Usermanager usermanager;

        // hämta lista från Workoutmanager
        public ObservableCollection<Workout> Workouts { get; }

        // Skapa en CollectionView för filtrering
        public ICollectionView WorkoutsView { get; set; }

        // Lista för typer av träning 
        public ObservableCollection<string> WorkoutTypes { get; set; }


        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout)); 
            }
        }

        private Workout newWorkout;

        public Workout NewWorkout
        {
            get { return newWorkout; }
            set
            {
                newWorkout = value;
                OnPropertyChanged(nameof(NewWorkout));
            }
        }

        // används för databindning till användarnamnet
        public Person User
        {
            get
            {
                return usermanager.CurrentUser;
            }
        }

        private string selectedWorkoutType;
        public string SelectedWorkoutType
        {
            get { return selectedWorkoutType; }
            set
            {
                selectedWorkoutType = value;
                OnPropertyChanged(nameof(SelectedWorkoutType));
                WorkoutsView.Refresh();
            }
        }

        private DateTime? filterDate; // Nullable för att hantera inget valt datum
        public DateTime? FilterDate
        {
            get { return filterDate; }
            set
            {
                filterDate = value;
                OnPropertyChanged();
                WorkoutsView.Refresh(); // Uppdatera vyn vid ändring
            }
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                WorkoutsView.Refresh(); // Uppdatera listan direkt när söktext ändras
            }
        }

        // uppdatera det nya användarnamnet från UserDetailsWindow
        public void UpdateUserName()
        {
            OnPropertyChanged(nameof(User));
        }

        public RelayCommand AddWorkOutCommand { get; }
        public RelayCommand RemoveWorkoutCommand { get; }
        public RelayCommand OpenUserDetailsCommand { get; }

        public RelayCommand OpenWorkoutDetailsWindowCommand { get; }
        public RelayCommand InfoCommand { get; }

        public RelayCommand SignOutCommand { get; }
        public RelayCommand CopyCommand { get; } 
        public RelayCommand SaveNewWorkoutCommand { get; }

        public RelayCommand SearchCommand { get; }


        // Konstruktor
        public WorkoutWindowViewModel(Usermanager usermanager) 
        {
            this.usermanager = usermanager;

            // hämta lista med träningspass från workoutmanager
            Workouts = usermanager.WorkoutManager.WorkoutList;

            // Hämta workout typer från workout manager
            WorkoutTypes = new ObservableCollection<string>(usermanager.WorkoutManager.WorkoutTypes);

            // Skapa en ICollectionView baserat på Workouts
            WorkoutsView = CollectionViewSource.GetDefaultView(Workouts);
            WorkoutsView.Filter = FilterWorkouts;

            // Lägg till "All" till WorkoutTypes
            WorkoutTypes.Insert(0, "All");

            NewWorkout = new CardioWorkout();
            NewWorkout = new StrengthWorkout();

            AddWorkOutCommand = new RelayCommand(AddWorkOut);
            OpenUserDetailsCommand = new RelayCommand(ExecuteOpenDetails);
            OpenWorkoutDetailsWindowCommand = new RelayCommand(ExecuteOpenWorkoutDetails);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkOut);
            InfoCommand = new RelayCommand(ShowInfo);
            SignOutCommand = new RelayCommand(SignOut);
            CopyCommand = new RelayCommand(CopyWorkout);
            SaveNewWorkoutCommand = new RelayCommand(SaveNewWorkout);
            SearchCommand = new RelayCommand(SearchingForWorkout);
        }

        // Metoder

        private bool FilterWorkouts(object obj)
        {
            if (obj is Workout workout)
            {
                // Kolla om datummatchning
                bool matchesDate = FilterDate == null || workout.Date.Date == FilterDate.Value.Date;

                // Kolla om typmatchning
                bool matchesType = string.IsNullOrEmpty(SelectedWorkoutType) || SelectedWorkoutType == "All" || workout.Type == SelectedWorkoutType;

                // Kolla om söktexten matchar någon av workoutens egenskaper
                bool matchesSearchText = string.IsNullOrEmpty(SearchText) ||
                                         workout.Type.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         workout.Notes.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;

                return matchesDate && matchesType && matchesSearchText; // Alla kriterier måste vara sanna
            }
            return false;
        }

        private void SearchingForWorkout(object parameter)
        {
            // Konvertera parameter till söksträng
            if (parameter is string searchText)
            {
                SearchText = searchText;
                WorkoutsView.Refresh(); // Uppdatera vyn vid sökning
            }
        }

        private void AddWorkOut(object parameter)
        {
            Application.Current.MainWindow.Close();

            var addWindow = new AddWorkoutWindow(usermanager);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }

        private void CopyWorkout(object parameter)
        {
            if (SelectedWorkout == null)
            {
                MessageBox.Show("Please select a workout to copy.");
                return;
            }

            // Skapa en ny instans av NewWorkout beroende på vilken Type SelectedWorkout har
            NewWorkout = SelectedWorkout is CardioWorkout cardio
                ? new CardioWorkout
                {
                    Date = cardio.Date,
                    Type = cardio.Type,
                    Duration = cardio.Duration,
                    CaloriesBurned = cardio.CaloriesBurned,
                    Notes = cardio.Notes
                }
                : new StrengthWorkout
                {
                    Date = ((StrengthWorkout)SelectedWorkout).Date,
                    Type = ((StrengthWorkout)SelectedWorkout).Type,
                    Duration = ((StrengthWorkout)SelectedWorkout).Duration,
                    CaloriesBurned = ((StrengthWorkout)SelectedWorkout).CaloriesBurned,
                    Notes = ((StrengthWorkout)SelectedWorkout).Notes
                };

            Application.Current.MainWindow.Close();

            // Öppna CopyFönster
            var copyWindow = new CopyWorkoutDetailsWindow(this);
            Application.Current.MainWindow = copyWindow;
            copyWindow.Show();
        }

        private void SaveNewWorkout(object parameter)
        {

            if (NewWorkout != null)
            {
                // Lägg till nytt träningspass till listan
                usermanager.WorkoutManager.AddWorkout(NewWorkout);
                MessageBox.Show("Workout copied and saved successfully!");

                OpenWorkoutWindow();
            }
            else
            {
                MessageBox.Show("No workout to save.");
            }
        }

        private void OpenWorkoutWindow()
        {
            // Skapa en ny instans av UserDetailsWindow
            var workoutWindow = new WorkoutWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = workoutWindow;
            workoutWindow.Show();
        }

        private void RemoveWorkOut(object parameter)
        {
            if (selectedWorkout == null)
            {
                MessageBox.Show("You need to select a activity.");
                return;
            }
            else
            {
                // tar bort ett specifikt träningspass markerat i listan
                usermanager.WorkoutManager.WorkoutList.Remove(selectedWorkout);
            }
        }

        private void ExecuteOpenDetails(object parameter)
        {
            OpenDetails(selectedWorkout);
        }

        // Metod för att öppna UserDetailsWindow
        private void OpenDetails(Workout workout)
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(usermanager, this);

            //Application.Current.MainWindow.Close();

            Application.Current.MainWindow = userDetailsWindow;
            userDetailsWindow.Show();
        }

        private void ExecuteOpenWorkoutDetails(object parameter)
        {
            if (selectedWorkout == null)
            {
                MessageBox.Show("No workout selected.");
                return;
            }
            else
            {
                OpenWorkoutDetailsWindow(selectedWorkout);
            }
        }
        private void OpenWorkoutDetailsWindow(Workout workout) // skickar Workout till WorkoutDetailsWindow
        {
            // Skapa en instans av WorkoutDetailsWindow 
            var workoutDetailsWindow = new WorkoutDetailsWindow(workout, usermanager);

            Application.Current.MainWindow.Close();

            Application.Current.MainWindow = workoutDetailsWindow;
            workoutDetailsWindow.Show();
        }

        private void ShowInfo(object parameter)
        {
            // Skapar ett nytt fönster och sätt page FitTrackInfo som innehåll
            Window infoPage = new Window
            {
                Content = new FitTrackInfo(),
                Title = "Information About FitTrack",
                Height = 450,
                Width = 800
            };

            // Visa page
            infoPage.Show();
        }

        private void SignOut(object parameter)
        {
            try
            {
                // Stäng alla öppna fönster
                foreach (Window window in Application.Current.Windows)
                {
                    window.Hide();
                }

                // Skapa en ny instans av MainWindow
                MainWindow mainWindow = new MainWindow(usermanager);
                mainWindow.Show(); 

                // Sätt det nya fönstret som huvudfönster
                Application.Current.MainWindow = mainWindow;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during sign out: {ex.Message}");
            }
        }
    }
}
