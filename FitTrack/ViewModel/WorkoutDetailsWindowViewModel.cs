using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {

        private WorkoutManager workoutManager;

        // Bindningsbar lista för träningspass
        public ObservableCollection<Workout> WorkoutList { get; }

        // Egenskaper
        private Workout workout;

        public Workout Workout
        {
            get { return workout; }
            set 
            { 
                workout = value;
                OnPropertyChanged();
            }
        }


        // Konstruktor
        public WorkoutDetailsWindowViewModel()
        {
            this.workoutManager = new WorkoutManager();
        }

        // skapat tillfällig metod för att testa
        public WorkoutDetailsWindowViewModel(WorkoutManager workoutManager)
        {
            // Sätt WorkoutList till workoutManager’s WorkoutList
            //WorkoutList = workoutManager.WorkoutList; // Detta visar alla aktiviteter i listan men det ska visas specifik info om den valda aktiviteten!
        }

        // Metoder
        private void EditWorkout()
        {

        }

        private void SaveWorkout()
        {
            // TILLFÄLLIG
            if (workout == null)
            {
                
            }
            else 
            {
                //workout = new Workout();
            }
        }
    }
}
