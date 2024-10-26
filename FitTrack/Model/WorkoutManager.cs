﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitTrack.Model
{
    public class WorkoutManager 
    {
        // Lista som innehåller workout objekt
        public ObservableCollection<Workout> WorkoutList { get; set; }

        // Tillfällig
        Workout workout;

        // Egenskaper


        // Konstruktor
        public WorkoutManager()
        {
            WorkoutList = new ObservableCollection<Workout>
            {
                new CardioWorkout { Date = new DateTime(2024, 11, 01, 18, 30, 0), Type = "Spinning" },
                new StrengthWorkout { Date = new DateTime(2024, 11, 02, 17, 45, 0), Type = "Bodypump" }
            };

            //WorkoutList.Add(new CardioWorkout { Date = new DateTime(2024, 11, 01, 18, 30, 0), Type = "Spinning" });
            //WorkoutList.Add(new StrengthWorkout { Date = new DateTime(2024, 11, 02, 17, 45, 0), Type = "Bodypump" });
        }

        // Metoder
        public bool CheckWorkout(string workoutType)
        {
            // går igenom och jämför varje träningspass i WorkoutList
            foreach (var activity in WorkoutList)
            {
                if (activity.Type == workoutType)
                {
                    return true; // Om träningspasset hittas, returnera true
                }
            }
            return false; // Om träningspasset inte hittas, returnera false
        }

        // TILLFÄLLIG TEST
        public bool UpdateWorkout(DateTime date, string type, TimeSpan duration, int CaloriesBurned, string notes)
        {
            foreach (var workout in WorkoutList)
            {
                if (workout.Date == date && workout.Type == type && workout.CaloriesBurned == CaloriesBurned && workout.Notes == notes)
                {
                    MessageBox.Show("UPDATE WORKOUT");
                    return true;
                }
            }
            return false;
        }

        public void AddWorkout(Workout newWorkout)
        {
            WorkoutList.Add(newWorkout);
        }

        // Metod för att hämta alla träninsgpass ?? 
        public ObservableCollection<Workout> GetAllWorkouts()
        {
            return WorkoutList;
        }
    }
}
