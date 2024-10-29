using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
		// Egenskaper
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
			set 			{ duration = value; }
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



		// Konstruktor
		public AddWorkoutWindowViewModel()
		{
			
		}

		// Metoder
		private void SaveWorkout()
		{

		}

	}
}
