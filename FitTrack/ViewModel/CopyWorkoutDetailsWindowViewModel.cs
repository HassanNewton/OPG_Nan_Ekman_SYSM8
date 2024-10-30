using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class CopyWorkoutDetailsWindowViewModel : ViewModelBase
    {
        // Klass för att kopiera träningspass för att skapa nytt träningspass

        Usermanager usermanager;

        private Workout newWorkout;

        public Workout NewWorkout
        {
            get { return newWorkout; }
            set 
            { 
                newWorkout = value;
                OnPropertyChanged(nameof(NewWorkout));
            }
        }

        public RelayCommand SaveNewWorkoutCommand { get; } 

        public CopyWorkoutDetailsWindowViewModel(Workout originalWorkout, Usermanager usermanager)
        {
            this.usermanager = usermanager;

            SaveNewWorkoutCommand = new RelayCommand(SaveNewWorkout);
        }

        private void SaveNewWorkout(object parameter)
        {
            if (NewWorkout != null)
            {
                usermanager.WorkoutManager.AddWorkout(NewWorkout); // Lägger till kopian i listan
                MessageBox.Show("Workout copied and saved successfully.");
            }
            else
            {
                MessageBox.Show("Failed to copy workout.");
            }
        }
    }
}
