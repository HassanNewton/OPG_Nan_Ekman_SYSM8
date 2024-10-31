using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    public class VerificationCodeWindowViewModel : ViewModelBase
    {
        Usermanager usermanager;
        public VerificationCodeWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;
        }
    }
}
