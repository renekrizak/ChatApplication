using ChatClient.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.ViewModel;

namespace ChatClient.Commands
{
    public class NavigateLoginViewCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;

        public NavigateLoginViewCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new LoginViewModel();   
        }
    }
}
