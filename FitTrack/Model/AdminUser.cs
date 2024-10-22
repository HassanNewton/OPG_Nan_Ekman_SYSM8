using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class AdminUser : User
    {
        public void ManageAllWorkouts()
        {
            Usermanager userManager = new Usermanager();
            // Hämta alla användare från Usermanager klassen? 
            userManager.GetAllUsers();
        }
    }
}
