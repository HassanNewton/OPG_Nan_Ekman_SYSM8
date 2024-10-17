using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack
{
    public abstract class Person
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public abstract void SignIn();


    }
}
