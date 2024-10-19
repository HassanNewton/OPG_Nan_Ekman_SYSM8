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
        // Egenskaper
        //public string LabelTitle { get; set; }

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

        //public ICommand SignInCommand { get; }

        public RelayCommand SignInCommand { get; }

        // Konstruktor
        public MainWindowViewModel()
        {
            SignInCommand = new RelayCommand(SignIn);
        }

        // Metoder
        private void SignIn(object parameter)
        {
            User user = new User
            {
                UserName = UsernameInput,
                Password = PasswordInput
            };

            if (user.UserName == "testuser" && user.Password == "1234")
            {
                MessageBox.Show($"Welcome {user.UserName}");
                // stäng main ich öppna UserDetailWindow
                OpenUserDetailWindow();
            }
            else
            {
                MessageBox.Show($"Invalid username or password");
            }
        }


        private void Register()
        {

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
    }
}
