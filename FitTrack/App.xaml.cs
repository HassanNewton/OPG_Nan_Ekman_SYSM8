using FitTrack.Model;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FitTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Usermanager UserManager { get; private set; } = new Usermanager();
        public static WorkoutManager Workoutmanager { get; private set; }
        public static string LoggedInUsername { get; set; } // FÖr att kunna se användarnamn vid inlogg
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Sätt explicit shutdown mode
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Initiera statiska instanser av UserManager och WorkoutManager
            UserManager = new Usermanager();
            Workoutmanager = new WorkoutManager();

            // Visa splash screen
            var splashScreen = new View.SplashScreen();
            splashScreen.Show();

            // Kort fördröjning för effekt (exempel)
            Thread.Sleep(1000);

            // Stäng splashskärmen
            splashScreen.Hide();

            // Starta mainWindow och visa det
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Sätt MainWindow som huvudfönster
            Application.Current.MainWindow = mainWindow;

            // Ändra shutdown-läget till OnLastWindowClose för att stänga appen när sista fönstret stängs
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
    }
}
