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
            base.OnStartup(e);  // Initiera applikationen korrekt

            // Skapa och visa splashskärmen
            var splashScreen = new View.SplashScreen();
            splashScreen.Show();

            // Kort fördröjning (4 sekunder)
            Thread.Sleep(4000);  // Simulera fördröjning för att visa splash-skärmen

            // Stäng splashskärmen
            splashScreen.Hide();

            // Starta mainWindow
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
