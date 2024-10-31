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
        Usermanager usermanager;

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
            this.usermanager = usermanager;

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
            Person currentUser = usermanager.LogIn(UsernameInput, PasswordInput); // Spara den inloggade användaren

            if (currentUser != null) // ändrat från loggedInPerson
            {
                if (currentUser is AdminUser)
                {
                    // Skapa AdminUser med Usermanager och WorkoutManager instanser
                    var adminUser = new AdminUser(usermanager);
                    OpenAdminFunctions(adminUser);
                }
                else if (currentUser is User user)
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
            AdminWindow adminWindow = new AdminWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = adminWindow;
            adminWindow.Show();
        }

        private bool ValidateUser(string username, string password)
        {
            // foreach-loop för att leta efter matchande användare
            foreach (var user in usermanager.Users)
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
            RegisterWindow registerWindow = new RegisterWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = registerWindow;
            registerWindow.Show();
        }

        private void OpenWorkoutWindow(User loggedInUser)
        {
            // Skapa en ny instans av WorkoutWindow
            WorkoutWindow workoutWindow = new WorkoutWindow(usermanager);

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
            // Skapa en ny instans av NewPasswordWindow
            NewPasswordWindow newPasswordWindow = new NewPasswordWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = newPasswordWindow;
            newPasswordWindow.Show();

        }
    }
}
