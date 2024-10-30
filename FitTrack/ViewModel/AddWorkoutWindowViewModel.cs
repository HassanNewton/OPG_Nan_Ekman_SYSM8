using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
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
                OnPropertyChanged(nameof(CaloriesBurned));
			}
		}

		private string notesInput;

		public string NotesInput
		{
			get { return notesInput; }
			set 
			{ 
				notesInput = value; 
				OnPropertyChanged();
			}
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
            if (string.IsNullOrEmpty(SelectedWorkoutType) || Duration == TimeSpan.Zero || CaloriesBurned <= 0 || string.IsNullOrEmpty(NotesInput))
            {
                MessageBox.Show("All fields must be filled out correctly.");
                return;
            }

            Workout workoutToAdd;

            if (SelectedWorkoutType == "Cardio")
            {
                workoutToAdd = new CardioWorkout
                {
                    Date = DateTime.Now,
                    Duration = Duration,
                    CaloriesBurned = CaloriesBurned,
                    Notes = NotesInput,
                    Type = SelectedWorkoutType
                };
            }
            else
            {
                workoutToAdd = new StrengthWorkout
                {
                    Date = DateTime.Now,
                    Duration = Duration,
                    CaloriesBurned = CaloriesBurned,
                    Notes = NotesInput,
                    Type = SelectedWorkoutType
                };
            }

            usermanager.WorkoutManager.AddWorkout(workoutToAdd);
            MessageBox.Show("Workout added!");

            // Stäng fönstret efter att workouten har sparats
            OpenWorkoutWindow();
        }

        private void OpenWorkoutWindow()
        {
            // Skapa en ny instans av UserDetailsWindow
            var workoutWindow = new WorkoutWindow(usermanager);

            // Close the current main window
            Application.Current.MainWindow.Close();

            // Set the new window as the main window and show it
            Application.Current.MainWindow = workoutWindow;
            workoutWindow.Show();
        }

        // TA BORT DENNA? KALKYLERA AUTOMATISKT I SISTA UPPGIFTEN/FÖNSTRET?? 
        private void CalculateCalories()
		{
			if (SelectedWorkoutType == "Cardio")
			{
				var cardioworkout = new CardioWorkout
				{
					Duration = Duration
                };
				CaloriesBurned = cardioworkout.CalculateCaloriesBurned();
			}
			else if (SelectedWorkoutType == "Strength")
			{
				var strengthworkout = new StrengthWorkout
				{
					Duration = Duration
				};
				CaloriesBurned = strengthworkout.CalculateCaloriesBurned();

            }
		}
	}
}
