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
        private WorkoutManager workoutManager;

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

        // KOnstruktor för att kunna skicka in parameter av  WorkoutManager
        public MainWindowViewModel(WorkoutManager workoutManager)
        {
            this.workoutManager = workoutManager;
        }

        // Tom konstruktor efter error från WorkoutDetailsViewModel
        public MainWindowViewModel()
        {

        }

        // Metoder
        private void SignIn(object parameter)
        {
            if (string.IsNullOrEmpty(UsernameInput) || string.IsNullOrEmpty(PasswordInput))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Loggar in användaren och returnerar ett Person-objekt
            Person loggedInPerson = userManager.LogIn(UsernameInput, PasswordInput);

            if (loggedInPerson != null)
            {
                // Kontrollera om den inloggade personen är en admin
                if (loggedInPerson is AdminUser adminUser)
                {
                    OpenAdminFunctions(adminUser);
                }
                else if (loggedInPerson is User user)
                {
                    // Hantera vanliga användare
                    OpenWorkoutWindow(user);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        // Metod för att hantera access för AdminUser
        private void OpenAdminFunctions(AdminUser adminUser)
        {
            // Visa en lista över användare eller träningspass

            // Tillfällig
            MessageBox.Show("Welcome, Admin! Here you can manage users and workouts.");

            // Anropar GetUserList TILLFÄLLIG
            adminUser.GetUserList(); 
        }

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

        private void OpenWorkoutWindow(User loggedInUser)
        {
            // Skapa en ny instans av WorkoutWindow
            WorkoutWindow workoutWindow = new WorkoutWindow(new WorkoutWindowViewModel(loggedInUser));

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
