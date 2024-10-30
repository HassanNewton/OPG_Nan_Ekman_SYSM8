using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public class WorkoutManager 
    {
        // Egenskaper

        // Lista som innehåller workout objekt
        public ObservableCollection<Workout> WorkoutList { get; set; }

        // Konstruktor
        public WorkoutManager()
        {
            WorkoutList = new ObservableCollection<Workout>
            {
                new CardioWorkout { Date = new DateTime(2024, 11, 01, 18, 30, 0), Duration = new TimeSpan(1,0,0), CaloriesBurned = 250, Notes = "Spinning", Type = "Cardio" },
                new StrengthWorkout { Date = new DateTime(2024, 11, 01, 18, 30, 0), Duration = new TimeSpan(1,0,0), CaloriesBurned = 300, Notes = "Bodypump", Type = "Strength"}
                //new CardioWorkout { Date = new DateTime(2024, 11, 01, 18, 30, 0), Type = "Spinning" },
                //new StrengthWorkout { Date = new DateTime(2024, 11, 02, 17, 45, 0), Type = "Bodypump" }
            };
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

        public bool UpdateWorkout(DateTime date, string type, TimeSpan duration, int CaloriesBurned, string notes)
        {
            foreach (var workout in WorkoutList)
            {
                if (workout.Date == date && workout.Type == type && workout.CaloriesBurned == CaloriesBurned && workout.Notes == notes)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddWorkout(Workout newWorkout)
        {
            WorkoutList.Add(newWorkout);
        }

        // Metod för att hämta alla träninsgpass
        public ObservableCollection<Workout> GetAllWorkouts()
        {
            return WorkoutList;
        }
    }
}
