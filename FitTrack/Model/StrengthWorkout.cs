﻿using System;
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
            // Exempelvärde: 40 kalorier per timme.
            int caloriesPerHour = 60; // Timme eller minuter?? 

            // Räkna ut förbrända kalorier baserat på distans
            int calculatedCalories = repetitions * caloriesPerHour;
            this.CaloriesBurned = calculatedCalories;

            return calculatedCalories;


        }
    }
}
