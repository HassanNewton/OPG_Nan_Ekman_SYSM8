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
            Duration = Duration;
        }

        public override int CalculateCaloriesBurned()
        {
            // Räkna ut förbrända kalorier baserat på distans 10 kalorier per kilometer plus 5 kalorier per minut
            int calculatedCalories = (Distance * 10) + ((int)Duration.TotalMinutes * 5);
            this.CaloriesBurned = calculatedCalories;

            return calculatedCalories; 
        }
        public override Workout CopyWorkout()
        {
            return new CardioWorkout
            {
                Date = this.Date,
                Type = this.Type,
                Duration = this.Duration,
                CaloriesBurned = this.CaloriesBurned,
                Notes = this.Notes,
                Distance = this.Distance
            };
        }
    }
}
