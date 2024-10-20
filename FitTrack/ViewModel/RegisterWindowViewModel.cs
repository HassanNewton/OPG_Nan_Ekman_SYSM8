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
    public class RegisterWindowViewModel : ViewModelBase // ändrat att ärva från MainWindow till ViewModelBase
    {
        // Ny Action för att stänga fönstret
        public Action CloseAction { get; set; }

        // Egenskaper för databindning genom att öppna upp alla set?
        public string UserInput { get; set; }
        public string PasswordInput { get; set; }   
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }

        // Skapa egenskap av Enum Countries
        private Countries selectedCountry;

        public Countries SelectedCountry
        {
            get { return selectedCountry; }
            set 
            { 
                if(selectedCountry != value)
                {
                    selectedCountry = value;
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        // Skapat instans av RelayCommand för att kunna binda i XAML genom att använda command sitället för clickEvent
        public RelayCommand RegisterCommand { get; }

        // Konstruktor
        public RegisterWindowViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterNewUser);
        }
      

        // Metod
        private void RegisterNewUser(object parameter)
        {

            OpenMainWindow();

            // Stäng fönstret genom att använda CloseAction
            CloseAction?.Invoke();
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
