using FitTrack.Model;
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
        // skapa ett objekt av Usermanager
        private Usermanager usermanager;

        // skapat en selectedUser av User objekt

        private User selectedUser;

        // Lista med securityQuestions till ComboBox
        public List<string> SecurityQuestion { get; set; }

        public User SelectedUser
        {
            get { return selectedUser; }
            set 
            
            { 
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        // Egenskaper
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

        //private string securityAnswerInput;
        //public string SecurityAnswerInput
        //{
        //    get { return securityAnswerInput; }
        //    set
        //    {
        //        securityAnswerInput = value;
        //        OnPropertyChanged();
        //    }
        //}


        public RelayCommand RegisterNewPasswordCommand { get; }
       //public RelayCommand ValidateUserCommand {  get; }

        // Konstruktor
        public NewPasswordWindowViewModel()
        {
            RegisterNewPasswordCommand = new RelayCommand(RegisterNewPassword);
            //ValidateUserCommand = new RelayCommand(ValidateUserSecurityQuestion);

            // skapat en lista av SecurityQuestion
            SecurityQuestion = new List<string> 
            { 
                "What was the name of your first pet?",
                "In what city were you born?",
                "What was your first teacher's name?",
            };

            usermanager = new Usermanager();
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
            if (SelectedUser == null)
            {
                MessageBox.Show("User not found");
            }
            else
            {
                // KOD-LOGIK
                OnPropertyChanged(nameof(SelectedUser));

                Application.Current.MainWindow.Close();
                OpenMainWindow();
            }
        }

        //private void ValidateUserSecurityQuestion(object parameter)
        //{
        //    // Användaren kommer behöva svara på en säkerhetsfråga för att validera att det är rätt användare   

        //    // Tillfälligt med MessageBox
        //    string message = "Was Alice your kindergarten teacher's name?";
        //    string title = "Security Question";
        //    MessageBoxButton buttons = MessageBoxButton.YesNo;

        //    // Använd MessageBoxResult istället för DialogResult
        //    MessageBoxResult result = MessageBox.Show(message, title, buttons);

        //    if (result == MessageBoxResult.Yes)
        //    {
        //        // Stäng fönstret om användaren svarade "Yes"
        //        MessageBox.Show("Correct! Welcome!");
        //    }
        //    else
        //    {
        //        // Om användare svarar "No"
        //        MessageBox.Show("Security check failed.");
        //    }
        //}

        private void FindUser()
        {

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
