using ChatClient.Commands;
using ChatClient.Store;
using System.Windows.Input;

namespace ChatClient.ViewModel
{
    public class LandingViewModel : ViewModelBase
    {

        public ICommand NavigateLoginViewCommand { get; }
        public ICommand NavigateRegisterViewCommand { get; }

        public LandingViewModel(NavigationStore navigationStore)
        {
            NavigateLoginViewCommand = new NavigateLoginViewCommand(navigationStore);
            NavigateRegisterViewCommand = new NavigateRegisterViewCommand(navigationStore);
        }
    }
}
