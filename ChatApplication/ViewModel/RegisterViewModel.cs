using ChatClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Store;
using ChatClient.Commands;
using System.Windows.Input;


namespace ChatClient.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
      
        public ICommand NavigateClientViewCommand { get; }
        public ICommand NavigateLoginViewCommand { get; }

        public RegisterViewModel(NavigationStore navigationStore)
        {
            NavigateClientViewCommand = new NavigateClientViewCommand(navigationStore);
            NavigateLoginViewCommand = new NavigateLoginViewCommand(navigationStore);
        }
     
    }
}
