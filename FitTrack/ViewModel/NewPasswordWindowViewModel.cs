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
        //// skapa ett objekt av Usermanager
        private Usermanager usermanager;

        // Lista med securityQuestions till ComboBox
        public List<string> SecurityQuestion { get; set; }

        // tillfällig - // skapat en selectedUser av User objekt
        private User selectedUser;
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

        public RelayCommand RegisterNewPasswordCommand { get; }

        // Konstruktor
        public NewPasswordWindowViewModel()
        {
            RegisterNewPasswordCommand = new RelayCommand(RegisterNewPassword);

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
            try
            {
                // Kontrollera om användarnamn, lösenord och confirm lösenord inte är tomma
                if (string.IsNullOrEmpty(UserInput) || string.IsNullOrEmpty(PasswordInput) ||
                    string.IsNullOrEmpty(ConfirmPasswordInput))
                {
                    MessageBox.Show("Please fill all the boxes.");
                    return;
                }
                // kollar om användare finns i Usermanager listan
                bool userExist = usermanager.CheckUsername(UserInput);
                if (!userExist)
                {
                    MessageBox.Show("User does not exist");
                    return;
                }
                if (ConfirmPasswordInput != PasswordInput)
                {
                    MessageBox.Show("Passwords does not match.");
                    return;
                }
                // Logik för att uppdatera/spara nya lösenordet för användaren

                //Nedan borde fungera?? Eller gör jag fel? Men användare kan inte logga in med det nya lösenordet
                bool updatedPassword = usermanager.UpdatePassword(UserInput, PasswordInput);
                if (updatedPassword)
                {
                    MessageBox.Show("Changed password");
                }
                else
                {
                    MessageBox.Show("Failed to update your password");
                }



                OpenMainWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenMainWindow()
        {
            // Skapa en ny instans av MainWindow
            MainWindow mainWindow = new MainWindow();

            // stäng befintligt fönster
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
