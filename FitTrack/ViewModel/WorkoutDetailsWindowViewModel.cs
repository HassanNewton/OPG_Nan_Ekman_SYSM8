﻿using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : MainWindowViewModel
    {
        // Egenskaper

        private Workout workout;

        public Workout Workout
        {
            get { return workout; }
            set { workout = value; }
        }


        // Konstruktor
        // skapat tillfällig metod för att testa
        public WorkoutDetailsWindowViewModel(Usermanager usermanager) : base(usermanager)
        {

        }

        // Metoder
        private void EditWorkout()
        {

        }

        private void SaveWorkout()
        {
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
