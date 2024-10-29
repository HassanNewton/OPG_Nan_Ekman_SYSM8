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
        // Refererar till Usermanager klassen 
        Usermanager usermanager;
        WorkoutManager workoutmanager;

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

            CountryList = new List<string> { "Denmark", "Norway", "Sweden" };
            RegisterUserCommand = new RelayCommand(RegisterNewUser);
        }

        // Metod
        private void RegisterNewUser(object parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(UserInput) || string.IsNullOrEmpty(PasswordInput) ||
                    string.IsNullOrEmpty(ConfirmPasswordInput) || string.IsNullOrEmpty(CountryComboBox))
                {
                    MessageBox.Show("Please enter both username, password and country.");
                    return;
                }
                if (ConfirmPasswordInput != PasswordInput)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }
                if (!ValidatePasswordRequirements(PasswordInput))
                {
                    MessageBox.Show("The password must be at least 8 characters, contain at least one letter, one number, and one special character.");
                    return;
                }
                if (usermanager.CheckUsername(UserInput)) 
                {
                    MessageBox.Show("Username already exists.");
                    return;
                }

                User newUser = new User
                {
                    UserName = UserInput,
                    Password = PasswordInput
                };
                usermanager.AddUser(newUser);

                MessageBox.Show($"New user created: {newUser.UserName}");

                // Öppna MainWindow efter registrering
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
