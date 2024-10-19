using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public abstract class Person
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set 
            { 
                userName = value; 
            }
        }

        public string Password { get; set; }

        public abstract void SignIn();


    }
}
