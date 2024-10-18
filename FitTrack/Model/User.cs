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
        public string SecurityQuestion { get; set; } // VG

        private string SecurityAnswer; // VG
        public string securityAnswer
        {
            get
            {
                return securityAnswer;
            }
            set
            {
                securityAnswer = value;
                // OnPropertyChanged();
            }// VG
        }

        // Konstruktor ??


        // Metoder

        public override void SignIn()
        {
            if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                Console.WriteLine("Användarnamn eller lösenord får inte vara tomt");
            }
        }

        public void ResetPassword(string securityAnswer)
        {
            if(securityAnswer == SecurityAnswer)
            {
                Console.WriteLine("Återställ lösenord");
            }
            else
            {
                Console.WriteLine("");
            }
        }
    }
}
