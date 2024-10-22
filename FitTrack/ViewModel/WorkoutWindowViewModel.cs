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
        public RelayCommand OpenDetailsCommand { get; }

        // Konstruktor
        public WorkoutWindowViewModel()
        {
            WorkoutList = new List<Workout>();

            AddWorkOutCommand = new RelayCommand(AddWorkOut);
            //OpenDetailsCommand = new RelayCommand(OpenDetails()); // funkar ej då jag har parameter Workout workout
        }

        // Metoder
        private void AddWorkOut(object parameter)
        {
            // Skapa en ny instans av WorkoutWindow
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = userDetailsWindow;
            userDetailsWindow.Show();
        }

        private void RemoveWorkOut()
        {

        }

        private void OpenDetails(Workout workout)
        {

        }

    }
}
