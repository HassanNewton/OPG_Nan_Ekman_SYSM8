using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private Usermanager userManager = new Usermanager();

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
                //OnPropertyChanged(); // behövs ej? 
            }
        }

        // Lista med länder till ComboBox
        public List<string> CountryList { get; set; }

        // Skapat instans av RelayCommand för att kunna binda i XAML genom att använda command sitället för clickEvent
        public RelayCommand RegisterUserCommand { get; }

        // Konstruktor
        public RegisterWindowViewModel()
        {
            // skapar en instans av Usermanager
            userManager = new Usermanager();

            // skapat en lista av Countries
            CountryList = new List<string> { "Denmark", "Norway", "Sweden" };

            RegisterUserCommand = new RelayCommand(RegisterNewUser);
        }

        // Metod
        private void RegisterNewUser(object parameter)
        {
            // bool för att kontrollera om en användare finns eller inte?? 

            if(ConfirmPasswordInput != PasswordInput)
            {
                MessageBox.Show("Passwords does not match.");
                return;
            }
            // Kontrollerar om anvärdarnamet finns
            if (userManager.CheckUsername(UserInput)) 
            {
                MessageBox.Show("Username already exist.");
            }

            // Kontrollera om användarnamn, lösenord och land inte är tomma
            if (string.IsNullOrEmpty(UserInput) || string.IsNullOrEmpty(PasswordInput) ||
                string.IsNullOrEmpty(ConfirmPasswordInput) || string.IsNullOrEmpty(CountryComboBox))
            {
                MessageBox.Show("Please enter both username, password and country.");
                return;
            }
            else
            {
                //      logik för att spara ny användare i listan.
                User newUser = new User();
                {
                    newUser = UserInput;       // Sätt användarnamn
                    Password = PasswordInput;     // Sätt lösenord
                }
                userManager.AddUser(newUser);

                MessageBox.Show($"New user created {.UserName}"); // användare hämtas inte från User

                Application.Current.MainWindow.Close();
                OpenMainWindow();
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

        private void PasswordRequirements()
        {
            // Lösenordet måste uppfylla särskilda krav(minst 8 tecken, minst en siffra och ett specialtecken).
            // Ett fält för att bekräfta lösenordet ska läggas till, där båda fälten måste matcha innan registrering tillåts.


        }
    }
}
