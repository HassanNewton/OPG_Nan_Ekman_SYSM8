﻿using FitTrack.Model;
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
        private ObservableCollection<Person> Users { get; set; } // testa ObservableCollection

        // Egenskaper
        //public string LabelTitle { get; set; } // tillfälligt utkommenterad

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

        // Skapat instans av RelayCommand för att kunna binda i XAML
        public RelayCommand SignInCommand { get; }
        public RelayCommand RegisterCommand {  get; }

        // Konstruktor
        public MainWindowViewModel()
        {
            Users = new ObservableCollection<Person>(); // testa ObservableCollection

            Users.Add(new User(){ UserName="user1", Password="1234"});           
            Users.Add(new AdminUser() { UserName = "adminUser", Password = "admin123" });

            //new User() { UserName = "user1", Password = "password" }; // testa ObservableCollection
            //new User() { UserName = "user2", Password = "password" };      // testa ObservableCollection          

            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
        }

        // Metoder

        private Person selectedUser; // tillfällig för att testa ObservableCollection
        public Person SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged(); // anropas så fort värdet ändras
            }
        }
        private void SignIn(object parameter)
        {
            
            // test nedan för att se att sign in funkar. SPARA I LISTA ISTÄLLET??
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
    }
}