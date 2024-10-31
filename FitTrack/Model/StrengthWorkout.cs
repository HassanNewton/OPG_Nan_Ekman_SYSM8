using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        private int repetitions;

        public int Repetitions
        {
            get { return repetitions; }
            set 
            { 
                repetitions = value;                
            }
        }

        // Konstruktor
        public StrengthWorkout()
        {
            // Tom eftersom min workoutmanager ska kunna hämta utan parametrar,kanske spara passen på annat sätt? 
        }
        public StrengthWorkout(int repetitions, TimeSpan duration)
        {
            Repetitions = repetitions;
            Duration = duration;
        }

        public override int CalculateCaloriesBurned()
        {
            // Räkna ut förbrända kalorier baserat på distans 8 kalorier per repetition plus 4 kalorier per minut
            int calculatedCalories = (Repetitions * 8) + ((int)Duration.TotalMinutes * 4);
            this.CaloriesBurned = calculatedCalories;

            return calculatedCalories;
        }

        public override Workout CopyWorkout()
        {
            return new StrengthWorkout
            {
                Date = this.Date,
                Type = this.Type,
                Duration = this.Duration,
                CaloriesBurned = this.CaloriesBurned,
                Notes = this.Notes,
                Repetitions = this.Repetitions
            };
        }
    }
}
