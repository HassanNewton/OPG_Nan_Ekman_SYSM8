using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    // Använd en manager-klass för att hålla koll på användare och nuvarande inloggade användare o.s.v.

    public class Usermanager
    {
        // Egenskap för lista av användare
        public ObservableCollection<Person> Users { get; set; } = new ObservableCollection<Person>();

        // lista för länder
        public List<string> CountryList { get; } = new List<string> { "Denmark", "Norway", "Sweden" };


        // Egenskap av Workoutmanager
        public WorkoutManager WorkoutManager { get; set; }

        // skapa nuvarande inloggad användare för bindning
        public Person CurrentUser { get; private set; }

        // Konstruktor
        public Usermanager()
        {
            WorkoutManager = new WorkoutManager();

            // Initierar Users som en ObservableCollection lista av Person-objekt med fördefinierade användare
            Users = new ObservableCollection<Person>
            {
            new User { UserName = "user1", Password = "1234" },
            new User { UserName = "user2", Password = "5678" },
            new AdminUser { UserName = "adminUser", Password = "admin123" }
            };

            // Gemensam country lista

        }

        // Metoder
        public bool CheckUsername(string username)
        {
            // går igenom och jämför varje användare i Users-listan
            foreach (var user in Users)
            {
                if (user.UserName == username)
                {
                    return true; // Om användarnamnet hittas, returnera true
                }
            }
            return false; // Om användarnamnet inte hittas, returnera false
        }

        public bool UpdatePassword(string userName, string newPassword)
        {
            foreach (var user in Users)
            {
                if (user.UserName == userName)
                {
                    user.Password = newPassword;
                    return true;
                }
            }
            return false;
        }
                
        public void AddUser(Person newUser) 
        {
            Users.Add(newUser);
        }


        // Metod för att hämta alla användare
        public ObservableCollection<Person> GetAllUsers()
        {
            return Users;
        }

        public Person LogIn(string username, string password)
        {
            foreach (var user in Users)
            {
                if (user.UserName == username && user.Password == password)
                {
                    CurrentUser = user;  // Sätt CurrentUser vid lyckad inloggning
                    return user;
                }
            }
            return null;  // Returnera null om inloggningen misslyckas
        }

    }
}
