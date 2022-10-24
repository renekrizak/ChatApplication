using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChatClient.Commands;
using ChatClient.Model;
using ChatClient.Store;
using ChatClient.View;

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
                
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if(!string.Equals(_password, value))
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
