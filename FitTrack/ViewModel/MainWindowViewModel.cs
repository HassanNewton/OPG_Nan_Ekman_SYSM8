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
        public RelayCommand OpenVerificationCodeCommand { get; }

        // Konstruktor
        public MainWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;

            SignInCommand = new RelayCommand(SignIn, CanSignIn);
            RegisterCommand = new RelayCommand(Register);
            NewPasswordCommand = new RelayCommand(NewPassword);
            OpenVerificationCodeCommand = new RelayCommand(OpenVerificationCodeWindow);
        }

        // Metoder

        private bool CanSignIn(object parameter)
        {
            // Kontrollera att användarnamn och lösenord är angivna
            return !string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput);
        }

        private void SignIn(object parameter)
        {
            // Kontrollera om kommandot kan köras
            if (!CanSignIn(parameter))
            {
                return; // Avsluta om det inte kan köras
            }

            // Logga in användaren
            Person currentUser = usermanager.LogIn(UsernameInput, PasswordInput);

            if (currentUser != null)
            {
                if (currentUser is User user)
                {

                    if (IsUserVerified(user))
                    {
                        OpenWorkoutWindow(user); // Öppna WorkoutWindow om användaren är verifierad
                    }
                    else
                    {
                        OpenVerificationCodeWindow(null); // Öppna verifieringsfönstret om inte
                    }
                    //// Öppna verifieringsfönstret om inloggningen lyckas
                    //OpenVerificationCodeWindow(null);
                }
                else if (currentUser is AdminUser adminUser)
                {
                    OpenAdminFunctions(adminUser);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
        //private void SignIn(object parameter)
        //{
        //    if (string.IsNullOrEmpty(UsernameInput) || string.IsNullOrEmpty(PasswordInput))
        //    {
        //        MessageBox.Show("Please enter both username and password.");
        //        return;
        //    }

        //    // Logga in användaren och returnera ett Person-objekt
        //    Person currentUser = usermanager.LogIn(UsernameInput, PasswordInput); // Spara den inloggade användaren

        //    if (currentUser != null)
        //    {
        //        if (currentUser is User user)
        //        {
        //            // anropar metod för att öppna verifieringsfönstret
        //            OpenVerificationCodeWindow(null); 
        //        }
        //        else if (currentUser is AdminUser adminUser)
        //        {
        //            OpenAdminFunctions(adminUser);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid username or password.");
        //    }
        //}

        private bool IsUserVerified(User user)
        {
            // Implementera logik för att kontrollera om användaren är verifierad
            // Det kan vara en egenskap på User-objektet eller en extern verifiering
            return user.IsVerified; // Anta att IsVerified är en egenskap på User
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

        private void OpenVerificationCodeWindow(object parameter)
        {
            // Skapa en ny instans av VerificationCodeWindow
            VerificationCodeWindow verificationCodeWindow = new VerificationCodeWindow(usermanager);

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = verificationCodeWindow;
            verificationCodeWindow.Show();
        }
    }
}
