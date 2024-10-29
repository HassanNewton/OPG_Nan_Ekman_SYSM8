using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    public class AdminWindowViewModel : ViewModelBase
    {
        Usermanager usermanager;

        public AdminWindowViewModel(Usermanager usermanager)
        {
            this.usermanager = usermanager;
        }


    }
}
