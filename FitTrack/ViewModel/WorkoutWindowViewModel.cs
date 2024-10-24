using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //Usermanager usermanager = new Usermanager();
        Usermanager usermanager;

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

        public RelayCommand AddWorkOutCommand { get; }
        public RelayCommand OpenUserDetailsCommand { get; }
        public RelayCommand OpenWorkoutDetailsWindowCommand { get; }

        public RelayCommand RemoveWorkoutCommand {  get; } // Öppna upp get? 

        public RelayCommand InfoCommand { get; }
        public RelayCommand SignOutCommand { get; }

        // Konstruktor
        public WorkoutWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;

            AddWorkOutCommand = new RelayCommand(AddWorkOut);
            // OpenUserDetailsCommand = new RelayCommand(OpenDetails); // HUR SKAPAR JAG RELAY NÄR OpenDetails() skickar med (Workout workout)?? 
            OpenWorkoutDetailsWindowCommand = new RelayCommand(OpenWorkoutDetailsWindow);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkOut);
            InfoCommand = new RelayCommand(GetInfo);
            SignOutCommand = new RelayCommand(SignOut);
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
            // tar bort ett specifikt träningspass markerat i listan
            // WorkoutList.Remove(selectedWorkout); // selectedWorkout?
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

        private void GetInfo(object parameter)
        {
            // Liten "info"-knapp som poppar upp en liten ruta där man kan läsa om hur man använder appen och FitTrack som företag

            // MessageBox eller nytt fönster/page? 
            MessageBox.Show("INFORMATION OM FITTRACK");
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
