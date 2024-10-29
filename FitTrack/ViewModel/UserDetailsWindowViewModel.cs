using FitTrack;
using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper

        Usermanager usermanager; 

        private string usernameInput;

        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();
            }
        }

        private string passwordInput;

        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged();
            }
        }

        private string confirmPasswordInput;

        public string ConfirmPasswordInput
        {
            get { return confirmPasswordInput; }
            set
            {
                confirmPasswordInput = value;
                OnPropertyChanged();
            }
        }

        private string countryComboBox;

        public string CountryComboBox
        {
            get { return countryComboBox; }
            set
            {
                countryComboBox = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveCommand { get; }

        // Konstruktor
        public UserDetailsWindowViewModel(Usermanager usermanager)
        {
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(SaveUserDetails);
        }

        // Metoder
        private void SaveUserDetails(object parameter)
        {

        }

        private void Cancel(object parameter)
        {
            Application.Current.MainWindow.Close();
            OpenWorkoutsWindow();
        }


        private void OpenWorkoutsWindow()
        {
            // Skapa en ny instans av MainWindow
            WorkoutWindow workoutsWindow = new WorkoutWindow(usermanager);

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = workoutsWindow;
            workoutsWindow.Show();
        }
    }
}
