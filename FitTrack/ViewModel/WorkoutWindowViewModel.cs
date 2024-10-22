using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
    {
        // Egenskaper
        private User user;

        public User User
        {
            get { return user; }
            set 
            { 
                user = value; 
                OnPropertyChanged(nameof(user));
            }
        }

        // Lista som innehåller workout objekt
        private List <Workout> WorkoutList { get; set; }

        public RelayCommand AddWorkOutCommand { get; }
        public RelayCommand OpenUserDetailsCommand { get; }
        public RelayCommand OpenWorkoutDetailsWindowCommand { get; }

        // Konstruktor
        public WorkoutWindowViewModel()
        {
            WorkoutList = new List<Workout>();

            AddWorkOutCommand = new RelayCommand(AddWorkOut);
            //OpenUserDetailsCommand = new RelayCommand(OpenDetails); 
            OpenWorkoutDetailsWindowCommand = new RelayCommand(OpenWorkoutDetailsWindow);
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

        private void RemoveWorkOut()
        {

        }

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

        private void OpenWorkoutDetailsWindow(object parameter)
        {
            // Skapa en ny instans av WorkoutDetailsWindow
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = workoutDetailsWindow;
            workoutDetailsWindow.Show();
        }
    }
}
