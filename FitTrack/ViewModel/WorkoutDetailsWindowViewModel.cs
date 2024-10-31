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
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper

        Usermanager usermanager;

        public ObservableCollection<string> WorkoutTypes { get; }

        private Workout workout;

        public Workout Workout
        {
            get { return workout; }
            set
            {
                workout = value;
                OnPropertyChanged(nameof(Workout));
            }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
                OnPropertyChanged(); 
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand EditCommand { get; }



        // Konstruktor
        public WorkoutDetailsWindowViewModel(Workout workout, Usermanager usermanager)
        {
            Workout = workout; // Spara den valda träningen
            this.usermanager = usermanager;
            IsEditing = false;

            WorkoutTypes = usermanager.WorkoutManager.WorkoutTypes;

            SaveCommand = new RelayCommand(SaveWorkout);
            EditCommand = new RelayCommand(EditWorkout);

        }

        // Metoder
        private void EditWorkout(object parameter)
        {
            IsEditing = true;
        }

        private void SaveWorkout(object parameter)
        {
            if (Workout.Date == null || string.IsNullOrEmpty(Workout.Type) ||
                Workout.Duration == TimeSpan.Zero || string.IsNullOrEmpty(Workout.Notes))
            {
                MessageBox.Show("Textbox cannot be empty.");
                return;
            }

            Workout.CalculateCaloriesBurned();

            usermanager.WorkoutManager.WorkoutList.Add(Workout);

            IsEditing = false; // Stäng av redigeringsläge
            OpenWorkoutWindow();
        }

        private void OpenWorkoutWindow()
        {
            // Skapa en ny instans av WorkoutWindow
            WorkoutWindow workoutWindow = new WorkoutWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = workoutWindow;
            workoutWindow.Show();
        }
    }
}
