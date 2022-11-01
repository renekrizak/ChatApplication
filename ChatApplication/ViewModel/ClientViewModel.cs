using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using ChatClient.Commands;
using ChatClient.Model;
using ChatClient.Store;
using ChatClient.View;
using ChatClient.Net;
using System.Net.Sockets;
using ChatClient.Core;


namespace ChatClient.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {
        public RelayCommand SendMessage { get; set; }
        private string _username;
        private string _password;
        private string _email;
        private string _uid;
        private string _message;
        private Server _server;
        
        public string Message
        {
            get { return _message; }
            set
            {
                Debug.WriteLine(Message);
                _message = value;
                ObjPropertyChanged();
            }
        }
        
        public ICommand SendMessageCommand { get; }

        public ClientViewModel(string username, string password)
        {
            _username = username;
            _password = password;
            Debug.WriteLine($"{_username}|{_password}");
            string logInfo = $"{_username}|{_password}";
            _server = new Server();
            _server.LoginConnectToServer(logInfo);
            SendMessage = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }

        public ClientViewModel(string username, string email, string password)
        {
            _username = username;
            _email = email;
            _password = password;
            Debug.WriteLine($"{_username}|{_email}|{_password}");
            string regInfo = $"{_username}|{_email}|{_password}";
            _server = new Server();
            _server.RegisterConnectToServer(regInfo);
        }

    }
}
