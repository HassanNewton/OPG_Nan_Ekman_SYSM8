using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class NewPasswordWindowViewModel : ViewModelBase
    {

        private string userInput;

        public string UserInput
        {
            get { return userInput; }
            set
            {
                userInput = value;
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

        private string validateUser;

        public string ValidateUser
        {
            get { return validateUser; }
            set 
            { 
                validateUser = value; 
                OnPropertyChanged();
            }
        }


        public RelayCommand RegisterNewPasswordCommand { get; }
        public RelayCommand ValidateUserCommand {  get; }

        // Konstruktor
        public NewPasswordWindowViewModel()
        {
            RegisterNewPasswordCommand = new RelayCommand(RegisterNewPassword);
            ValidateUserCommand = new RelayCommand(ValidateUserSecurityQuestion);
        }

        // Metoder
        private void RegisterNewPassword(object parameter)
        {
            if (ConfirmPasswordInput != PasswordInput)
            {
                MessageBox.Show("Passwords does not match.");
            }
            // Kontrollera om användarnamn, lösenord och confirm lösenord inte är tomma
            if (string.IsNullOrEmpty(UserInput) || string.IsNullOrEmpty(PasswordInput) ||
                string.IsNullOrEmpty(ConfirmPasswordInput))
            {
                MessageBox.Show("Please fill all the boxes.");
                return;
            }
            else
            {
                // KOD-LOGIK

                Application.Current.MainWindow.Close();
                OpenMainWindow();
            }
        }

        private void ValidateUserSecurityQuestion(object parameter)
        {
            // Användaren kommer behöva svara på en säkerhetsfråga för att validera att det är rätt användare   

            // Tillfälligt med MessageBox
            string message = "Was Alice your kindergarten teacher's name?";
            string title = "Security Question";
            MessageBoxButton buttons = MessageBoxButton.YesNo;

            // Använd MessageBoxResult istället för DialogResult
            MessageBoxResult result = MessageBox.Show(message, title, buttons);

            if (result == MessageBoxResult.Yes)
            {
                // Stäng fönstret om användaren svarade "Yes"
                MessageBox.Show("Correct! Welcome!");
            }
            else
            {
                // Om användare svarar "No"
                MessageBox.Show("Security check failed.");
            }
        }

        private void OpenMainWindow()
        {
            // Skapa en ny instans av MainWindow
            MainWindow mainWindow = new MainWindow();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
