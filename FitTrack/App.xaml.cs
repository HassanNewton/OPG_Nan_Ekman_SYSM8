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
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);  // Initiera applikationen korrekt

            //// Skapa och visa splashskärmen
            //var splashScreen = new View.SplashScreen();
            //splashScreen.Show();

            //// skapa instans av Usermanager
            //Usermanager usermanager = new Usermanager();

            //// Kort fördröjning (4 sekunder)
            //Thread.Sleep(1000);  // Simulera fördröjning för att visa splash-skärmen

            //// Stäng splashskärmen
            //splashScreen.Hide();

            //// Starta mainWindow
            //var mainWindow = new MainWindow(usermanager);
            //mainWindow.Show();


            // MED NEDAN STÄNGS MAIN VID REGISTRERING AV ANVÄNDARE
            base.OnStartup(e);

            // Ändra shutdown-läget till explicit, så applikationen inte stängs när MainWindow stängs
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Skapa och visa splashskärmen
            var splashScreen = new View.SplashScreen();
            splashScreen.Show();

            // Kort fördröjning (exempel)
            Thread.Sleep(1000);

            // Stäng splashskärmen
            splashScreen.Hide();

            // Starta mainWindow
            var usermanager = new Usermanager();
            var mainWindow = new MainWindow(usermanager);
            mainWindow.Show();

            // Sätt huvudfönstret
            Application.Current.MainWindow = mainWindow;
        }
    }
}
