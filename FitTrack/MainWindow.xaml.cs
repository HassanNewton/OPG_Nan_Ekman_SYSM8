﻿using FitTrack.ViewModel;
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

namespace FitTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowViewModel();
            //MainWindowViewModel viewModel = new MainWindowViewModel();
            //DataContext = viewModel;
            //ViewModel.MainWindowViewModel mainWindowViewModel = new ViewModel.MainWindowViewModel();
            //DataContext = mainWindowViewModel;

            DataContext = new MainWindowViewModel();
        }
    }
}
