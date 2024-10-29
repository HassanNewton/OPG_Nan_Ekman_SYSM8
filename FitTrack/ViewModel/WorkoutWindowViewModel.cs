using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // Egenskap för inloggad användare - Använde User loggedInUser för förtydligande istället för User user 
        private User loggedInUser;

        public User LoggedInUser
        {
            get { return loggedInUser; }
            set 
            { 
                loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }

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

        public RelayCommand AddWorkOutCommand { get; }
        public RelayCommand RemoveWorkoutCommand { get; }
        public RelayCommand OpenUserDetailsCommand { get; }

        public RelayCommand OpenWorkoutDetailsWindowCommand { get; }
        public RelayCommand InfoCommand { get; }
        private RelayCommand signOutCommand;

        public RelayCommand SignOutCommand { get; }

        // Konstruktor
        public WorkoutWindowViewModel(Usermanager usermanager) // ändrat så konstruktor tar parameter Usermanager istället för Workoutmanager
        {
            this.usermanager = usermanager;

            // hämta lista från workoutmanager
            Workouts = usermanager.WorkoutManager.WorkoutList;

            AddWorkOutCommand = new RelayCommand(AddWorkOut);
            OpenUserDetailsCommand = new RelayCommand(ExecuteOpenDetails);
            OpenWorkoutDetailsWindowCommand = new RelayCommand(ExecuteOpenWorkoutDetails);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkOut);
            InfoCommand = new RelayCommand(ShowInfo);
            SignOutCommand = new RelayCommand(SignOut);
        }

        // Konstruktor för att visa den inloggades användarnamn FUNKAR EJ 
        //public WorkoutWindowViewModel(User loggedInUser)
        //{
        //    LoggedInUser = loggedInUser;
        //    Workouts = new ObservableCollection<Workout>();
        //}

        // Metoder
        private void AddWorkOut(object parameter)
        {
            // Skapa en ny instans av AddWorkoutWindow
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = addWorkoutWindow;
            addWorkoutWindow.Show();
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
            // Skapa en ny instans av UserDetailsWindow
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = userDetailsWindow;
            userDetailsWindow.Show();
        }

        // Hur selectar jag workout? 
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
            // Skapa en instans av WorkoutDetailsWindow med workoutManager som parameter
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(workout);

            // Stäng WorkoutWindow (det nuvarande fönstret)
            Application.Current.MainWindow.Close();

            // Sätt WorkoutDetailsWindow som det nya huvudfönstret och visa det
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

            // Visa fönstret
            infoPage.Show();
        }

        private void SignOut(object parameter)
        {
            // Skapa en ny instans av MainWindow
            MainWindow mainWindow = new MainWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
