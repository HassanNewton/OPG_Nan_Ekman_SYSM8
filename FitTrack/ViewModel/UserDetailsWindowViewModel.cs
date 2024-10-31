using FitTrack;
using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper

        Usermanager usermanager;

        private WorkoutWindowViewModel workoutWindowViewModel;

        // list länder från usermanager
        public List<string> CountryList { get; private set; }

        // Fick inte till bindningen med att byta namn på inloggad användare trots skapat egenskaper med propfull
        // så tog hjälp av ChatGPT med kodraden nedan samt hur jag skulle kunna öppna fönster nya då det sätt jag gjort tidigare av någon anledning inte fungerade
        public Person CurrentUser => usermanager.CurrentUser; 

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
        public UserDetailsWindowViewModel(Usermanager usermanager, WorkoutWindowViewModel workoutWindowViewModel) // lagt till workoutwindow som parameter
        {
            this.usermanager = usermanager;
            this.workoutWindowViewModel = workoutWindowViewModel; 

            CountryList = usermanager.CountryList;

            // Initialisera UsernameInput med den nuvarande användarens namn
            UsernameInput = CurrentUser.UserName;

            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(SaveUserDetails);
        }

        // Metoder
        private void SaveUserDetails(object parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(UsernameInput) || string.IsNullOrEmpty(PasswordInput) ||
                    string.IsNullOrEmpty(ConfirmPasswordInput) || string.IsNullOrEmpty(CountryComboBox))
                {
                    MessageBox.Show("Please enter both username, password and country.");
                    return;
                }
                if (!ValidateUsernameRequirements(UsernameInput))
                {
                    MessageBox.Show("Username must be at least 3 characters long.");
                    return;
                }

                MessageBox.Show($"Username updated: {UsernameInput}");

                if (ConfirmPasswordInput != PasswordInput)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }
                if (!ValidatePasswordRequirements(PasswordInput))
                {
                    MessageBox.Show("The password must be at least 5 characters.");
                    return;
                }
                if (usermanager.CheckUsername(UsernameInput))
                {
                    MessageBox.Show("Username already exist.");
                    return;
                }

                // Uppdaterar det aktuella användarnamnet i Usermanager
                usermanager.CurrentUser.UserName = UsernameInput;

                // Anropar metoden för att uppdatera användarnamnet i WorkoutWindowViewModel
                workoutWindowViewModel.UpdateUserName();

                MessageBox.Show($"Username updated: {UsernameInput}");

                Application.Current.MainWindow.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object parameter)
        {
            Application.Current.MainWindow.Close();
        }


        private void OpenWorkoutsWindow()
        {
            // Skapa en ny instans av MainWindow
            WorkoutWindow workoutsWindow = new WorkoutWindow(usermanager);

            // stäng nuvarande fönster  
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = workoutsWindow;
            workoutsWindow.Show();
        }

        private bool ValidateUsernameRequirements(string UsernameInput)
        {
            // Användarnamnet måste vara minst 3 tecken långt.
            var usernameRequirement = @"^.{3,}$";
            return Regex.IsMatch(UsernameInput, usernameRequirement);
        }

        private bool ValidatePasswordRequirements(string PasswordInput)
        {
            // Lösenordet måste vara minst 5 tecken långt.
            var passwordLengthRequirement = @"^.{5,}$";
            return Regex.IsMatch(PasswordInput, passwordLengthRequirement);
        }

        private void ExecuteOpenDetails(object parameter)
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(usermanager, workoutWindowViewModel);
            userDetailsWindow.Show();
        }
    }
}
