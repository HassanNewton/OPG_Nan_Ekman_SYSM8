using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        User user = new User();

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
            // skapat en lista av Countries
            CountryList = new List<string> { "Denmark", "Norway", "Sweden" };

            RegisterUserCommand = new RelayCommand(RegisterNewUser);
        }

        // Metod
        private void RegisterNewUser(object parameter)
        {
            if(ConfirmPasswordInput != PasswordInput)
            {
                MessageBox.Show("Passwords does not match.");
            }

            // Om användarnamnet redan är upptaget ska ett varningsmeddelande visas.
            if(user.UserName == UserInput) // funkar ej
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
                //      Kan jag kalla på listan från MainWindowViewModel på något sätt?

                MessageBox.Show($"New user created {user.UserName}"); // användare hämtas inte från User

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
    }
}
