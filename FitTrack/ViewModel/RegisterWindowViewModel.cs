using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace FitTrack.ViewModel
{

    /*Möjligheten att skapa en ny användare med användarnamn, lösenord och land.
     Ska ha följande funktionaliteter:

    •Inputrutor för att fylla i användarnamn och lösenord för användaren.
    •ComboBox för att välja land för användaren.
    •"Register"-knapp för att skapa en användare och komma tillbaka till MainWindow och stänga RegisterWindow.
    •Om användarnamnet redan är upptaget ska ett varningsmeddelande visas.
     */
    public class RegisterWindowViewModel : ViewModelBase // ändrat från att ärva från MainWindow till ViewModelBase
    {

        //User user = new User();

        // Refererar till Usermanager klassen, hur hämtar jag listan?? 
        //private Usermanager userManager = new Usermanager();
        Usermanager usermanager;

        // Egenskaper för databindning 
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

        private string countryComboBox;

        public string CountryComboBox
        {
            get { return countryComboBox; }
            set 
            { 
                countryComboBox = value; 
            }
        }

        // Lista med länder till ComboBox
        public List<string> CountryList { get; set; }

        // Skapat instans av RelayCommand för att kunna binda i XAML genom att använda command sitället för clickEvent
        public RelayCommand RegisterUserCommand { get; }

        // Konstruktor
        public RegisterWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;

            // skapar en instans av Usermanager
            //userManager = new Usermanager();

            // skapat en lista av Countries
            CountryList = new List<string> { "Denmark", "Norway", "Sweden" };

            RegisterUserCommand = new RelayCommand(RegisterNewUser);
        }

        // Metod
        private void RegisterNewUser(object parameter)
        {
            try
            {
                // Kontrollera om användarnamn, lösenord och land inte är tomma
                if (string.IsNullOrEmpty(UserInput) || string.IsNullOrEmpty(PasswordInput) ||
                    string.IsNullOrEmpty(ConfirmPasswordInput) || string.IsNullOrEmpty(CountryComboBox))
                {
                    MessageBox.Show("Please enter both username, password and country.");
                    return;
                }
                if (ConfirmPasswordInput != PasswordInput)
                {
                    MessageBox.Show("Passwords does not match.");
                    return;
                }
                if (!ValidatePasswordRequirements(PasswordInput))
                {
                    MessageBox.Show("The password must be at least 8 characters, contains at least one letter (upper or lower case), at least one number and one special character.");
                    return;
                }
                // Kontrollerar om anvärdarnamet finns
                if (usermanager.CheckUsername(UserInput))
                {
                    MessageBox.Show("Username already exist.");
                    return;
                }

                // logik för att spara ny användare i listan.
                User newUser = new User();
                {
                    newUser.UserName = UserInput;       // Sätt användarnamn
                    newUser.Password = PasswordInput;     // Sätt lösenord
                }
                usermanager.AddUser(newUser);

                MessageBox.Show($"New user created {newUser.UserName}");

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
            MainWindow mainWindow = new MainWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        private bool ValidatePasswordRequirements(string PasswordInput)
        {
            // Lösenordet måste uppfylla särskilda krav(minst 8 tecken, minst en siffra och ett specialtecken).
            var passwordRequirements = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
            return Regex.IsMatch(PasswordInput, passwordRequirements);
        }
    }
}
