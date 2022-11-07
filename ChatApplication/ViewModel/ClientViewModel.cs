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
using System.Collections.ObjectModel;

namespace ChatClient.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {
        
        public ObservableCollection<UserModel> users { get; set; }
        
        public ObservableCollection<MessageModel> userMessages { get; set; }

        private string _username;
        private string _password;
        private string _email;
        public string UID { get; set; }
        private string _message;
        private Server _server;

       
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                ObjPropertyChanged();
                Task.Run(() => { Debug.WriteLine($"Message text: {Message}"); });
            }
        }

        public ICommand SendMessage { get; }
        public ClientViewModel(string username, string password)
        {
            userMessages = new ObservableCollection<MessageModel>();
            users = new ObservableCollection<UserModel>();
            _username = username;
            _password = password;
            Debug.WriteLine($"{_username}|{_password}");
            string logInfo = $"{_username}|{_password}";
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.disconnectedEvent += UserDisconnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.IDReceivedEvent += IDReceived;
            _server.LoginConnectToServer(logInfo);
            SendMessage = new SendMessage(_server);
            
            
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
            UID = _server._packetReader.readMessage();
            Debug.WriteLine($"User UID: {UID}");
        }

        private void  UserConnected()
        {
            var user = new UserModel
            {
                Username = _server._packetReader.readMessage(),
            };

            if(!users.Any(x => x.Username == user.Username))
            {
                Application.Current.Dispatcher.Invoke(() => users.Add(user));
            }
        }

        private void UserDisconnected()
        {
           var usrname = _server._packetReader.readMessage();
            var remove = users.Where(x => x.Username == usrname).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => users.Remove(remove));
        }

        private void MessageReceived()
        {
            var userMessage = new MessageModel
            {
                messageUsernme = _server._packetReader.readMessage(),
                messageContent = _server._packetReader.readMessage()
            };

            var msg = _server._packetReader.readMessage();
            Application.Current.Dispatcher.Invoke(() => userMessages.Add(userMessage));

        }

    }

}
