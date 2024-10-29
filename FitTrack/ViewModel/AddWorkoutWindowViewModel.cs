using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
		// Egenskaper
		Usermanager usermanager;

		private string workoutTypeComboBox;

		public string WorkoutTypeComboBox
		{
			get { return workoutTypeComboBox; }
			set 
			{ 
				workoutTypeComboBox = value;
				OnPropertyChanged();
			}
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

		private string notesInput;

		public string NotesInput
		{
			get { return notesInput; }
			set { notesInput = value; }
		}

        public RelayCommand SaveWorkoutCommand { get; }

        // Konstruktor
        public AddWorkoutWindowViewModel(Usermanager usermanager)
		{
			this.usermanager = usermanager;

            SaveWorkoutCommand = new RelayCommand(SaveWorkout);

        }

		// Metoder
		private void SaveWorkout(object parameter)
		{
            if (string.IsNullOrEmpty(WorkoutTypeComboBox) || Duration == TimeSpan.Zero || CaloriesBurned == 0 || string.IsNullOrEmpty(NotesInput))
            {
                MessageBox.Show("Textbox cannot be empty.");
                return;
            }
            else
            {
                Application.Current.MainWindow.Close();
            }
        }

	}
}
