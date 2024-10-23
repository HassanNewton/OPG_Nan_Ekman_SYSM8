using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class User : Person 
    {
        // Egenskaper
        public string Country { get; set; }

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
                //OnPropertyChanged();
            }
        }



        // Konstruktor (osäker om jag ska ha alla parametrar i konstruktorn)
        public User()
        {

        }


        // Metoder

        public override void SignIn()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                Console.WriteLine("Username or password can not be empty");
            }
            else
            {
                // Validera inloggningsuppgifterna
                if (UserName == "user" && Password == "password")
                {
                    Console.WriteLine($"Login succeded! {UserName}");
                }
                else
                {
                    Console.WriteLine("Wrong username or password");
                }
            }
        }

        public void ResetPassword(string providedAnswer)
        {
            if(SecurityAnswer == providedAnswer)
            {
                Console.WriteLine("Password reset succeded.");
            }
            else
            {
                Console.WriteLine("Wrong security answer.");
            }
        }
    }
}
