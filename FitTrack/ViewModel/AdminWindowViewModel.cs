using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    public class AdminWindowViewModel : ViewModelBase
    {
        private Usermanager usermanager;
        private WorkoutManager workoutManager;

        public AdminWindowViewModel()
        {
            // Använder de statiska instanserna direkt från App
            this.usermanager = App.UserManager;
            this.workoutManager = App.Workoutmanager;

            
        }


    }
}
