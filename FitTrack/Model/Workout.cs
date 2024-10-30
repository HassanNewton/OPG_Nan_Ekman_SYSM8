using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public abstract class Workout
    {
        // Egenskaper 
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private TimeSpan duration;

        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        private int caloriesBurned;

        public int CaloriesBurned
        {
            get { return caloriesBurned; }
            set { caloriesBurned = value; }
        }

        private string notes;

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        // Konstruktor
        public Workout() 
        { 
        
        }

        // Metoder
        public abstract int CalculateCaloriesBurned();

        //// Abstrakt metod för att kopiera träningspass
        public abstract Workout CopyWorkout();

    }
}
