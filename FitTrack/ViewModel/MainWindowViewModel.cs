using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Refererar till Usermanager klassen
        private Usermanager userManager;

        // Egenskaper
        private string usernameInput;
        private string passwordInput;

        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();
            }
        }

        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged();
            }
        }

        // Skapat instans av RelayCommand för att kunna binda knappar i XAML
        public RelayCommand SignInCommand { get; }
        public RelayCommand RegisterCommand {  get; }
        public RelayCommand NewPasswordCommand { get; }

        // Konstruktor
        public MainWindowViewModel(Usermanager usermanager)
        {
            // skapar en instans av Usermanager
            this.userManager = usermanager;

            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
            NewPasswordCommand = new RelayCommand(NewPassword);      
        }

        // Metoder

        // behöver jag ändra här med i min MainWindow för att användaren ska kunna använda det nya lösenordet från NewPasswordViewModel?
        private void SignIn(object parameter)
        {
            // Kontrollera om användarnamn och lösenord inte är tomma
            if (string.IsNullOrEmpty(UsernameInput) || string.IsNullOrEmpty(PasswordInput))
            {
                MessageBox.Show("Please enter both username and password.");
                // Avbryt exekveringen av metoden om användarnamn eller lösenord är tom
                return;
            }

            // validera användarnamn och lösenord
            if (ValidateUser(UsernameInput, PasswordInput))
            {
                MessageBox.Show($"Welcome {UsernameInput}");
                
                OpenWorkoutWindow();
            }
            else
            {
                // Om ingen användare hittas, visa felmeddelande
                MessageBox.Show("Invalid username or password.");
            }
        }

        // behöver jag ändra här med i min MainWindow för att användaren ska kunna använda det nya lösenordet från NewPasswordViewModel?

        private bool ValidateUser(string username, string password)
        {
            // foreach-loop för att leta efter matchande användare
            foreach (var user in userManager.Users)
            {
                if(user.UserName == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        private void Register(object parameter)
        {
            // Skapa en ny instans av RegisterWindow
            RegisterWindow registerWindow = new RegisterWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = registerWindow;
            registerWindow.Show();
        }

        private void OpenWorkoutWindow()
        {
            // Skapa en ny instans av WorkoutWindow
            WorkoutWindow workoutWindow = new WorkoutWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = workoutWindow;
            workoutWindow.Show();
        }

        private void NewPassword(object parameter)
        {
            OpenNewPasswordWindow();
        }

        private void OpenNewPasswordWindow()
        {
            // OBS OBS! 
            // Main stängs bara om jag trycker på knappen Forgot Password först när programmet startas,
            // men om jag registrerar användare och sedan väljer forgot password så stängs inte main

            // Skapa en ny instans av NewPasswordWindow
            NewPasswordWindow newPasswordWindow = new NewPasswordWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = newPasswordWindow;
            newPasswordWindow.Show();

        }
    }
}
