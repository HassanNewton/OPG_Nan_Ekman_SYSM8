﻿using FitTrack.Model;
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
    /// Interaction logic for NewPasswordWindow.xaml
    /// </summary>
    public partial class NewPasswordWindow : Window
    {
        public NewPasswordWindow(Usermanager usermanager)
        {
            InitializeComponent();
            ViewModel.NewPasswordWindowViewModel newPasswordWindowViewModel = new ViewModel.NewPasswordWindowViewModel(usermanager);
            DataContext = newPasswordWindowViewModel;
        }
    }
}
