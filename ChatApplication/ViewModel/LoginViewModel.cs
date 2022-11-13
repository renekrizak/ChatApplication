using ChatClient.Commands;
using ChatClient.Store;
using System.Windows.Input;

namespace ChatClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        private string _username;
        private string _password;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                ObjPropertyChanged();
                /*Task.Run(() =>
                {
                    Debug.WriteLine($"Login: {Formatted}");
                    Thread.Sleep(50);
                });*/

            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (!string.Equals(_password, value))
                {
                    _password = value;
                    ObjPropertyChanged();
                }
            }
        }

        public ICommand NavigateClientViewCommand { get; }
        public ICommand NavigateRegisterViewCommand { get; }

        public LoginViewModel(NavigationStore navigationStore)
        {

            NavigateClientViewCommand = new NavigateClientViewCommand(navigationStore);
            NavigateRegisterViewCommand = new NavigateRegisterViewCommand(navigationStore);
        }
    }
}
