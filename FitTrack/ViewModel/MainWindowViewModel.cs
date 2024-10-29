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

        // Gjorde om konstruktor eftersom jag både har usermanager och workoutmanager
        // Konstruktor som tar emot befintliga instanser eller skapar nya om de saknas
        public MainWindowViewModel(Usermanager userManager = null, WorkoutManager workoutManager = null)
        {
            // Använd den givna instansen eller skapa en ny
            this.userManager = userManager ?? new Usermanager();
            this.workoutManager = workoutManager ?? new WorkoutManager();

            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
            NewPasswordCommand = new RelayCommand(NewPassword);
        }

        // Metoder
        private void SignIn(object parameter)
        {
            if (string.IsNullOrEmpty(UsernameInput) || string.IsNullOrEmpty(PasswordInput))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Logga in användaren och returnera ett Person-objekt
            Person loggedInPerson = App.UserManager.LogIn(UsernameInput, PasswordInput); // Spara den inloggade användaren

            if (loggedInPerson != null)
            {
                if (loggedInPerson is AdminUser)
                {
                    // Skapa AdminUser med Usermanager och WorkoutManager instanser
                    var adminUser = new AdminUser(App.UserManager, App.Workoutmanager);
                    OpenAdminFunctions(adminUser);
                }
                else if (loggedInPerson is User user)
                {
                    OpenWorkoutWindow(user);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void OpenAdminFunctions(AdminUser adminUser)
        {
            OpenAdminWindow();
        }

        private void OpenAdminWindow()
        {
            // Skapa en ny instans av RegisterWindow
            AdminWindow adminWindow = new AdminWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = adminWindow;
            adminWindow.Show();
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
