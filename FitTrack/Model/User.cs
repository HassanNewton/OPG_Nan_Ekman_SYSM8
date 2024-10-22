﻿using System;
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
        public string SecurityQuestion { get; set; } 

        private string SecurityAnswer; 
        public string securityAnswer
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

        public void ResetPassword(string securityAnswer)
        {
            if(securityAnswer == SecurityAnswer)
            {
                Console.WriteLine("Password reset.");
            }
            else
            {
                Console.WriteLine("Wrong security answer.");
            }
        }
    }
}
