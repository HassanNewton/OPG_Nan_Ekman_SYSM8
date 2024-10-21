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
        private ObservableCollection<Person> Users { get; set; } 

        // Egenskaper
        //public string LabelTitle { get; set; } // tillfälligt utkommenterad, vad är detta?? 

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
        public MainWindowViewModel()
        {
            // Skapa lista med 3 fördefinierade användare
            Users = new ObservableCollection<Person>(); 
            {
                Users.Add(new User() { UserName = "user1", Password = "1234" });
                Users.Add(new User() { UserName = "user2", Password = "5678" });
                Users.Add(new AdminUser() { UserName = "adminUser", Password = "admin123" });
            };

            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
            NewPasswordCommand = new RelayCommand(NewPassword);
        }

        // Metoder
        private void SignIn(object parameter)
        {
            // Kontrollera om användarnamn och lösenord inte är tomma
            if (string.IsNullOrEmpty(UsernameInput) || string.IsNullOrEmpty(PasswordInput))
            {
                MessageBox.Show("Please enter both username and password.");
                // Avbryt exekveringen av metoden om användarnamn eller lösenord är tom
                return;
            }

            // foreach-loop för att leta efter matchande användare
            foreach (var user in Users)
            {
                if (user.UserName == UsernameInput && user.Password == PasswordInput)
                {
                    MessageBox.Show($"Welcome {user.UserName}");
                    OpenUserDetailWindow();
                    return; // Avbryt loopen och metoden om vi hittar en matchande användare
                }
            }

            // Om ingen användare hittas, visa felmeddelande
            MessageBox.Show("Invalid username or password.");
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

        private void OpenUserDetailWindow()
        {
            // Skapa en ny instans av UserDetailsWindow
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();

            // Stäng MainWindow
            Application.Current.MainWindow.Close();

            // Sätt det nya fönstret som huvudfönster och visa det
            Application.Current.MainWindow = userDetailsWindow;
            userDetailsWindow.Show();
        }

        // Tillfällig kod 
        private void NewPassword(object parameter)
        {
            // tillfälligt meddelande för att kolla att knappen fungerar
            //MessageBox.Show("Du har valt nytt lösenord");
            OpenNewPasswordWindow();
        }

        private void OpenNewPasswordWindow()
        {
            NewPasswordWindow newPasswordWindow = new NewPasswordWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = newPasswordWindow;
            newPasswordWindow.Show();
        }
    }
}
