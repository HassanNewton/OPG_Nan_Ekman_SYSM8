using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public class User : Person 
    {
        // Egenskaper
        public string Country { get; set; }

        public bool IsVerified { get; set; } // Ny egenskap för verifieringsstatus

        private string securityQuestion;

        public string SecurityQuestion
        {
            get { return securityQuestion; }
            set 
            { 
                securityQuestion = value;                
            }
        }

        private string securityAnswer; 
        public string SecurityAnswer
        {
            get
            {
                return securityAnswer;
            }
            set
            {
                securityAnswer = value;
            }
        }

        // Konstruktor
        public User()
        {

        }

        // KOnstruktor med parametrar sen när användare ska korrigeras?
        public User(string username, string password, string country, string securityQuestion, string securityAnswer)
        {
            UserName = username;
            Password = password;
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
        }

        // Metoder
        public override void SignIn()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Username or password can not be empty");
            }
            else
            {
                // Validera inloggningsuppgifterna
                if (UserName == "user" && Password == "password")
                {
                    MessageBox.Show($"Login succeded! {UserName}");
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
            }
        }

        public void ResetPassword(string providedAnswer)
        {
            if(SecurityAnswer == providedAnswer)
            {
                MessageBox.Show("Password reset succeded.");
            }
            else
            {
                MessageBox.Show("Wrong security answer.");
            }
        }
    }
}
