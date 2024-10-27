using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AdminUser(Usermanager usermanager)
        {
            this.usermanager = usermanager;  
            //userManager = new Usermanager(); // OBS OBS OBS!! Programmet kraschar när jag skapar ny instans av Usermanager i AdminUser

        }

        public void ManageAllWorkouts()
        {
            foreach (var workout in workoutmanager.GetAllWorkouts())
            {
                // KODLOGIK if?
            }
        }

        private void GetUserList()
        {
            // Hämta alla användare från Usermanager klassen? 
            usermanager.GetAllUsers();
        }

    }
}
