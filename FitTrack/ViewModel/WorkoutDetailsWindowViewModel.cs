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

        public Workout SelectedWorkout { get; set; } // om set är private kan jag inte öppna fönster för details

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
        public WorkoutDetailsWindowViewModel(Workout workout)
        {
            SelectedWorkout = workout; // Spara den valda träningen
            IsEditing = false;

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
            IsEditing = false; // Stäng av redigeringsläge
            MessageBox.Show("Your changes have been saved!");
        }
    }
}
