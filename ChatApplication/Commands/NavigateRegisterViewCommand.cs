using ChatClient.Store;
using ChatClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatClient.Commands
{
    public class NavigateRegisterViewCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateRegisterViewCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new RegisterViewModel();
        }

    }
}
