using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class AdminUser : User
    {
        // En admin-User ska kunna se/ta bort alla träningspass tillagda av användare
        
        Usermanager userManager;
        ObservableCollection<Person> users;

        User user;

        WorkoutManager workoutManager;
        Workout GetWorkouts;
        public ObservableCollection<Workout> Workouts { get; }

        // Konstruktor
        public AdminUser()
        {
            //userManager = new Usermanager(); // OBS OBS OBS!! Programmet kraschar när jag skapar ny instans av Usermanager i AdminUser
            user = new User();
        }

        // tillfällig Konstruktor
        public AdminUser(WorkoutManager workoutManager)
        {
            workoutManager = new WorkoutManager();
            user = new User();
        }


        public void ManageAllWorkouts()
        {
            workoutManager.GetAllWorkouts();

             // KODLOGIK 
        }

        private void GetUserList()
        {
            // Hämta alla användare från Usermanager klassen? 
            userManager.GetAllUsers();
        }

    }
}
