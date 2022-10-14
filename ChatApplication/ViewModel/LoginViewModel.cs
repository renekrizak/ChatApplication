using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatClient.Commands;
using ChatClient.Store;

namespace ChatClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand NavigateClientViewCommand { get; }
        public ICommand NavigateRegisterViewCommand { get; }
        
        public LoginViewModel(NavigationStore navigationStore)
        {
            NavigateClientViewCommand = new NavigateClientViewCommand(navigationStore);
            NavigateRegisterViewCommand = new NavigateRegisterViewCommand(navigationStore);
        }
    }
}
