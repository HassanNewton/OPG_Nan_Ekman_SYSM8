using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
    {
        // Refererar till Usermanager klassen
        private readonly Usermanager usermanager;

        // hämta lista från Workoutmanager
        public ObservableCollection<Workout> Workouts { get; }


        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout)); // anropas så fort värdet ändras
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

        // NY för att kunna uppdatera det nya användarnamnet från UserDetailsWindow
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

        // Konstruktor
        public WorkoutWindowViewModel(Usermanager usermanager) 
        {
            this.usermanager = usermanager; 

            // hämta lista från workoutmanager
            Workouts = usermanager.WorkoutManager.WorkoutList;

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
        }

        // Metoder
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

            //// Öppna UserDetailsWindow med referens till Usermanager och WorkoutWindowViewModel
            //UserDetailsWindow userDetailsWindow = new UserDetailsWindow(usermanager, this);
            
            //Application.Current.MainWindow = userDetailsWindow;
            //userDetailsWindow.Show();
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
            ////usermanager.CurrentUser = null;

            //// Skapa en ny instans av MainWindow
            //MainWindow mainWindow = new MainWindow(usermanager);

            //// Stäng MainWindow
            //Application.Current.MainWindow.Close();

            //// Sätt det nya fönstret som huvudfönster och visa det
            //Application.Current.MainWindow = mainWindow;
            //mainWindow.Show();

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
