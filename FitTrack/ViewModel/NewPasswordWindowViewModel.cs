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
        }

        private void ValidateUserSecurityQuestion(object parameter)
        {
            // Användaren kommer behöva svara på en säkerhetsfråga för att validera att det är rätt användare
            MessageBox.Show("QUESTION");
        }
    }
}
