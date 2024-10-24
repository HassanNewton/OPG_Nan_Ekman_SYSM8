using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
		// Egenskaper
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




	}
}
