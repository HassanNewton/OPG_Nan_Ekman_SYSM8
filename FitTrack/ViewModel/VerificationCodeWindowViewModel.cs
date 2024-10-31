using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    public class VerificationCodeWindowViewModel : ViewModelBase
    {
        // Egenskaper
        Usermanager usermanager;

        private string email;

        public string Email
        {
            get { return email; }
            set 
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string confirmEmail;

        public string ConfirmEmail
        {
            get { return confirmEmail; }
            set 
            { 
                confirmEmail = value; 
                OnPropertyChanged();
            }
        }

        private string codeInput;

        public string CodeInput
        {
            get { return codeInput; }
            set 
            {
                codeInput = value; 
                OnPropertyChanged();
            }
        }

        // egenskap för att lagra verifieringskod
        private string verificationCode;

        public RelayCommand GetCodeCommand { get; }
        public RelayCommand VerifyCodeCommand { get; }

        // Konstruktor
        public VerificationCodeWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;

            GetCodeCommand = new RelayCommand(ExecuteGetCode);
            VerifyCodeCommand = new RelayCommand(ExecuteVerification);
        }

        // Metoder

        private void ExecuteGetCode(object parameter)
        {
            if (Email != ConfirmEmail)
            {
                MessageBox.Show("Emails do not match. Please try again.");
                return;
            }

        }

        private void ExecuteVerification(object parameter)
        {
            if (CodeInput == verificationCode)
            {
                MessageBox.Show("Verification successful!");

            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.");
            }
        }

        private string GetVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Genererar en 6-siffrig kod
        }

        private void SendVerificationCode(string email, string code)
        {
            MessageBox.Show($"Verification code sent to {email}: {code}");
        }
    }
}
