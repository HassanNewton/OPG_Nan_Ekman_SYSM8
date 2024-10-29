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
    public class AdminWindowViewModel : ViewModelBase
    {
        // Egenskaper
        Usermanager usermanager;

        // hämta listorna från usermanager och workoutmanager
        public ObservableCollection<Person> Users { get; }
        public ObservableCollection<Workout> Workouts { get; }

        // Konstruktor
        public AdminWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;

            Users = new ObservableCollection<Person>();
            Workouts = new ObservableCollection<Workout>();
        }

        // Metoder

        public void DeleteWorkout(Workout workout)
        {

        }

    }
}
