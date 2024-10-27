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
using System.Windows.Navigation;

namespace FitTrack.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
    {

        // Refererar till Usermanager klassen
        private WorkoutManager workoutManager;

        private Usermanager usermanager;

        // hämta lista från Workoutmanager
        public ObservableCollection<Workout> Workouts { get; }

        // hämta lista från Usermanager
        public ObservableCollection<Person> GetUserList { get; }

        // Egenskaper
        private User user;

        public User User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(); // anropas så fort värdet ändras
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
        public WorkoutWindowViewModel(WorkoutManager workoutManager)
        {
            // skapa instans av WorkoutManager
            this.workoutManager = workoutManager;

            // hämta lista från workoutmanager
            Workouts = workoutManager.WorkoutList;

            AddWorkOutCommand = new RelayCommand(AddWorkOut);
            OpenUserDetailsCommand = new RelayCommand(ExecuteOpenDetails);
            OpenWorkoutDetailsWindowCommand = new RelayCommand(ExexuteOpenWorkoutDetails);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkOut);
            InfoCommand = new RelayCommand(ShowInfo);
            SignOutCommand = new RelayCommand(SignOut);
        }

        // Konstruktor för att visa den inloggades användarnamn
        public WorkoutWindowViewModel(string currentUser)
        {
            CurrentUser = currentUser;
            GetUserList = new ObservableCollection<Person>();
        }

        private string currentUser;

        public string CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }


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
                workoutManager.WorkoutList.Remove(selectedWorkout);
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
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = userDetailsWindow;
            userDetailsWindow.Show();
        }

        // Hur selectar jag workout? 
        private void ExexuteOpenWorkoutDetails(object parameter)
        {
            if (selectedWorkout == null)
            {
                MessageBox.Show("No workout selected.");
                return;
            }
            else
            {
                OpenWorkoutDetailsWindow(SelectedWorkout);
            }
        }

        private void OpenWorkoutDetailsWindow(object parameter)
        {
            // Skapa en instans av WorkoutDetailsWindow med workoutManager som parameter
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(workoutManager);

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
