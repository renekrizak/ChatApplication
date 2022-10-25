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

        /*private string _username;
        private string _password;
        private string _email;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                ObjPropertyChanged();
                
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                ObjPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                ObjPropertyChanged();
            }
        }*/
      
        public ICommand RegisterUserCommand { get; }
        public ICommand NavigateLoginViewCommand { get; }

        public RegisterViewModel(NavigationStore navigationStore)
        {
            RegisterUserCommand = new RegisterUserCommand(navigationStore);
            NavigateLoginViewCommand = new NavigateLoginViewCommand(navigationStore);
        }
     
    }
}
