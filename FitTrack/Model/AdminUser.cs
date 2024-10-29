using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public class AdminUser : User
    {
        // En admin-User ska kunna se/ta bort alla träningspass tillagda av användare

        //Skapa Relay för DeleteWorkout? Eller kan jag hämta från WorkoutWindow? 

        // Egenskaper
        private Usermanager usermanager;
        //private WorkoutManager workoutmanager;

        public ObservableCollection<Workout> Workouts { get; }

        // Konstruktor
        public AdminUser() // ingen parameter så att min lista från WorkoutManager ska funka
        {
            // måste jag skicka workoutmanager som parameter för att hämta lista från WorkoutManager klassen? 
        }

        public AdminUser(Usermanager usermanager)
        {
            this.usermanager = usermanager;
        }

        public void ManageAllWorkouts()
        {
            foreach (var workout in usermanager.WorkoutManager.GetAllWorkouts())
            {
                // KODLOGIK
            }
        }

        public void DeleteWorkout(Workout workout)
        {

            //workoutmanager.WorkoutList.Remove(workout);

            if (workout == null)
            {
                MessageBox.Show("No workout selected.");
                return;
            }

            if (usermanager.WorkoutManager.WorkoutList.Contains(workout))
            {
                usermanager.WorkoutManager.WorkoutList.Remove(workout);
                MessageBox.Show("Workout successfully removed.");
            }
            else
            {
                MessageBox.Show("The selected workout does not exist in the list.");
            }
        }

        public ObservableCollection<Person> GetUserList()
        {
            // returnerar user lista från usermanager
            return usermanager.GetAllUsers(); // App.UserManager eller bara usermanager?
        }

        public ObservableCollection<Workout> GetWorkoutsList()
        {
            //return workoutmanager.GetAllWorkouts();
            if (usermanager.WorkoutManager == null)
            {
                MessageBox.Show("WorkoutManager is not initialized.");
                return new ObservableCollection<Workout>(); // Returnerar tom lista om ej initialiserad
            }

            return usermanager.WorkoutManager.GetAllWorkouts();
        }

    }
}
