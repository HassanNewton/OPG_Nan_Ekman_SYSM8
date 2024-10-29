using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class CardioWorkout : Workout
    {
        private int distance;

        public int Distance
        {
            get { return distance; }
            set 
            { 
                distance = value; 
            }
        }

        // Konstruktor

        public CardioWorkout()
        {
            // Tom eftersom min workoutmanager ska kunna hämta utan parametrar,kanske spara passen på annat sätt? 
        }
        public CardioWorkout(int distance, TimeSpan duration)
        {
            Distance = distance;
            Duration = duration;
        }

        public override int CalculateCaloriesBurned()
        {
            // fast värde 50 kalorier per kilometer.
            int caloriesPerKm = 50;

            // Räkna ut förbrända kalorier baserat på distans
            int calculatedCalories = distance * caloriesPerKm;
            this.CaloriesBurned = calculatedCalories;

            return calculatedCalories; 

        }
    }
}
