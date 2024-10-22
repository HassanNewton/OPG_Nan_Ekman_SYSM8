using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    // Använd en manager-klass för att hålla koll på användare och nuvarande inloggade användare o.s.v.

    public class Usermanager
    {
        // Egenskap för lista av användare
        public ObservableCollection<Person> Users { get; set; }

        // Konstruktor
        public Usermanager()
        {
            // flyttat lista från MainWindowViewModel
            // Initierar Users som en ObservableCollection lista av Person-objekt med fördefinierade användare
            Users = new ObservableCollection<Person>
        {
            new User { UserName = "user1", Password = "1234" },
            new User { UserName = "user2", Password = "5678" },
            new AdminUser { UserName = "adminUser", Password = "admin123" }
        };
        }
    }
}
