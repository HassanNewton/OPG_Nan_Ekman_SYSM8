using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class AdminWindowViewModel : ViewModelBase
    {
        // Egenskaper
        Usermanager usermanager;

        // hämta listorna från usermanager och workoutmanager
        public ObservableCollection<Person> Users { get; }
        public ObservableCollection<Workout> Workouts { get; }

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

        private Person selectedUser;
        public Person SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        // Relaycommand för att kunna ta bort träningspass och användare med knapp
        public RelayCommand DeleteWorkoutCommand { get; }
        public RelayCommand DeleteUserCommand { get; }

        // Konstruktor
        public AdminWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;

            Users = usermanager.GetAllUsers();
            Workouts = usermanager.WorkoutManager.GetAllWorkouts();

            DeleteWorkoutCommand = new RelayCommand(DeleteWorkout);
            DeleteUserCommand = new RelayCommand(DeleteUser);
        }

        // Metoder
        private void DeleteWorkout(object parameter)
        {
            if (selectedWorkout == null)
            {
                MessageBox.Show("No workout selected.");
                return;
            }

            // Ta bort träningspasset från WorkoutManager och Workouts
            usermanager.WorkoutManager.WorkoutList.Remove(selectedWorkout);
            Workouts.Remove(selectedWorkout);
        }

        private void DeleteUser(object parameter)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("No user selected.");
                return;
            }

            // Ta bort användaren från Usermanager och Users
            usermanager.Users.Remove(selectedUser);
            Users.Remove(selectedUser);
        }

    }
}
