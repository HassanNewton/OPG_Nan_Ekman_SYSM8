﻿using FitTrack.Model;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitTrack.View
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow(Usermanager usermanager, WorkoutWindowViewModel workoutWindowViewModel)
        {
            InitializeComponent();
            ViewModel.UserDetailsWindowViewModel userDetailsWindowViewModel = new ViewModel.UserDetailsWindowViewModel(usermanager, workoutWindowViewModel);
            DataContext = userDetailsWindowViewModel;
        }
    }
}
