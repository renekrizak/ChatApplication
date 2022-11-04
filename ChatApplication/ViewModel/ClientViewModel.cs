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
using System.Collections.ObjectModel;

namespace ChatClient.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {
        public RelayCommand SendMessageCommand { get; set; }
        public ObservableCollection<UserModel> users { get; set; }
        public ObservableCollection<string> messages { get; set; }

        private string _username;
        private string _password;
        private string _email;
        public string UID { get; set; }
        private string _message;
        private Server _server;

        public string Message;
        
        
        public ClientViewModel(string username, string password)
        {
            users = new ObservableCollection<UserModel>();
            messages = new ObservableCollection<string>();
            _username = username;
            _password = password;
            
            Debug.WriteLine($"{_username}|{_password}");
            string logInfo = $"{_username}|{_password}";
            _server = new Server();
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(username), o => !string.IsNullOrEmpty(username));
            _server.connectedEvent += UserConnected;
            _server.disconnectedEvent += UserDisconnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.IDReceivedEvent += IDReceived;
            _server.LoginConnectToServer(logInfo);
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

        private void IDReceived()
        {
            UID = _server.PacketReader.readMessage();
            Debug.WriteLine($"User UID: {UID}");
        }

        private void  UserConnected()
        {
            var user = new UserModel
            {
                Username = _server.PacketReader.readMessage(),
            };

            if(!users.Any(x => x.Username == user.Username))
            {
                Application.Current.Dispatcher.Invoke(() => users.Add(user));
            }
        }

        private void UserDisconnected()
        {
           
        }

        private void MessageReceived()
        {
            var msg = _server.PacketReader.readMessage();
            Application.Current.Dispatcher.Invoke(() => messages.Add(msg));

        }

    }

}
