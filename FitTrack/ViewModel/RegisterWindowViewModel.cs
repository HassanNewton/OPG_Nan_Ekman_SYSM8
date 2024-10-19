using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class RegisterWindowViewModel : MainWindow
    {
        // Egenskaper
        public string UserInput { get; set; }
        public string PasswordInput { get; set; }   
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }

        // Metod
        private void ReisterNewUser()
        {

        }
    }
}
