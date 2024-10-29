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

        // Egenskaper
        private Usermanager usermanager;
        private WorkoutManager workoutmanager;

        // Konstruktor
        public AdminUser() // ingen parameter så att min lista från WorkoutManager ska funka
        {
            // måste jag skicka workoutmanager som parameter för att hämta lista från WorkoutManager klassen? 
        }

        public AdminUser(Usermanager usermanager, WorkoutManager workoutmanager)
        {
            if (usermanager == null)
                throw new ArgumentNullException(nameof(usermanager), "Usermanager cannot be null.");

            if (workoutmanager == null)
                throw new ArgumentNullException(nameof(workoutmanager), "WorkoutManager cannot be null.");

            this.usermanager = usermanager;
            this.workoutmanager = workoutmanager;
        }

        public void ManageAllWorkouts()
        {
            foreach (var workout in workoutmanager.GetAllWorkouts())
            {
                // KODLOGIK
            }
        }

        public ObservableCollection<Person> GetUserList()
        {
            // returnerar user lista från usermanager
            return usermanager.GetAllUsers();
        }

        public ObservableCollection<Workout> GetWorkoutsList()
        {
            //return workoutmanager.GetAllWorkouts();
            if (workoutmanager == null)
            {
                MessageBox.Show("WorkoutManager is not initialized.");
                return new ObservableCollection<Workout>(); // Returnerar tom lista om ej initialiserad
            }

            return workoutmanager.GetAllWorkouts();
        }

    }
}
