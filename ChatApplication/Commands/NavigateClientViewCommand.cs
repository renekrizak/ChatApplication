using ChatClient.Store;
using ChatClient.ViewModel;

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
            var values = (object[])parameter;
            // var conn = new Server();
            //conn.LoginConnectToServer(username);

            _navigationStore.CurrentViewModel = new ClientViewModel((string)values[0], (string)values[1], _navigationStore);
        }

    }
}
