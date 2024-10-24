using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public abstract class Workout
    {
        // Egenskaper ( Öppna upp alla??)
        public DateTime Date {get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        // Konstruktor
        public Workout() 
        { 
        
        }

        // Metoder
        public abstract int CalculateCaloriesBurned();

    }
}
