using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		Workout cardioworkout;
		Workout strengthworkout;

        public ObservableCollection<string> WorkoutTypes { get; set; }

        private string selectedWorkoutType;
        public string SelectedWorkoutType
        {
            get { return selectedWorkoutType; }
            set
            {
                selectedWorkoutType = value;
                CalculateCalories();
                OnPropertyChanged();
            }
        }

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
			set 
			{ 
				duration = value;
				CalculateCalories();
                OnPropertyChanged();
			}
		}

		private int caloriesBurned;

		public int CaloriesBurned
		{
			get { return caloriesBurned; }
			set 
			{ 
				caloriesBurned = value;
				CalculateCalories();
                OnPropertyChanged();
			}
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

            // skapa en lista att förvara Type för att binda till combobox
            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };

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
				MessageBox.Show("Workout added!");
			}          
        }

		private void CalculateCalories()
		{
			if (SelectedWorkoutType == "Cardio")
			{
				cardioworkout = new CardioWorkout
				{
					Duration = Duration
                };
				CaloriesBurned = cardioworkout.CalculateCaloriesBurned();
			}
			else if (SelectedWorkoutType == "Strength")
			{
				strengthworkout = new StrengthWorkout
				{
					Duration = Duration
				};
				CaloriesBurned = strengthworkout.CalculateCaloriesBurned();

            }
		}
	}
}
