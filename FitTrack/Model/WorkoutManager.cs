using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitTrack.Model
{
    public class WorkoutManager
    {
        // Lista som innehåller workout objekt
        private List<WorkoutManager> WorkoutList { get; set; }

        // Egenskaper


        // Konstruktor
        public WorkoutManager()
        {
            WorkoutList = new ObservableCollection<WorkoutManager>
            {
            new WorkoutManager { Date, Activity},
            };
        }

        // Metoder


    }
}
