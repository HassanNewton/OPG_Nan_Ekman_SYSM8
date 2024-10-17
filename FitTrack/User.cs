using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack
{
    public class User : Person
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; } // VG

        private string SecurityAnswer; // VG
        public string securityAnswer { get; set; } // VG

        public override void SignIn()
        {
            throw new NotImplementedException();
        }

        public void ResetPassword( string securityAnswer)
        {

        }
    }
}
