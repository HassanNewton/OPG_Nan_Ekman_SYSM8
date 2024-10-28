using FitTrack.Model;
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
    /// Interaction logic for WorkoutWindow.xaml
    /// </summary>
    public partial class WorkoutWindow : Window
    {
        public WorkoutWindow()
        {
            InitializeComponent();
            //Usermanager usermanager = new Usermanager();

            //WorkoutManager workoutManager = new WorkoutManager();
            //ViewModel.WorkoutWindowViewModel workoutWindowViewModel = new ViewModel.WorkoutWindowViewModel(workoutManager); // ändrat från usermanager till workoutManager i parametern
            //DataContext = workoutWindowViewModel;

        }

        public WorkoutWindow(WorkoutWindowViewModel viewModel) : this() // Anropar parameterlös konstruktor
        {
            DataContext = viewModel;
        }
    }
}
