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
            base.OnStartup(e);

            // Skapa och visa splashskärmen
            var splashScreen = new View.SplashScreen();
            splashScreen.Show();

            // Simulera en kort fördröjning (4 sekunder)
            System.Threading.Thread.Sleep(4000);

            // Öppna huvudfönstret
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Stäng splashskärmen
            splashScreen.Close();
        }
    }

}
