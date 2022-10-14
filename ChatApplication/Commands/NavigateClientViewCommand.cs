using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.ViewModel;
using ChatClient.Store;
namespace ChatClient.Commands
{
    public class NavigateClientViewCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateClientViewCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new ClientViewModel();
        }

    }
}
