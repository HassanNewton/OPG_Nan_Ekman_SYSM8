using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitTrack.MVVM
{

    /* RelayCommand Används för att skapa kommandon som kan bindas till UI-element, såsom knappar. 
     * Genom att använda denna kan man definiera både vad som ska ske när kommandot körs och om kommandot är aktiverat eller inte, vilket styrs av CanExecute.
     * */
    public class RelayCommand : ICommand // show potential fix och implement interface
    {
        // Fält för att hålla referenser till metoder som  definierar vad som ska göras
        // Action blir som en metod
        private Action<object> execute;

        // Kollar om kommandot kan köras
        // Func blir som en metod
        private Func<object, bool> canExecute;

        // Event som signalerar kommandots möjlighet att köras har ändrats, dvs. om knappen blivit aktiverad eller deaktiverad
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) // om ett kommande kan exekveras eller inte
        {
            // ska returna en bool om den kan exekveras eller inte
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter) // exekverar själva metoden
        {
            Console.WriteLine("RelayCommand executed");
            execute(parameter);
        }
    }
}