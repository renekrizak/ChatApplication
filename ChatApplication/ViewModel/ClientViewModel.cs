using ChatClient.Commands;
using ChatClient.Model;
using ChatClient.Net;
using ChatClient.Store;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ChatClient.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ICommand NavigateLoginViewCommand { get; }
        public ObservableCollection<UserModel> users { get; set; }

        public ObservableCollection<MessageModel> userMessages { get; set; }

        private string _username;
        private string _password;
        private string _email;
        public string UID { get; set; }
        private string _message;
        private Server _server;
       
        public string ClientUsername { get; set; }


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
        public ClientViewModel(string username, string password, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            userMessages = new ObservableCollection<MessageModel>();
            users = new ObservableCollection<UserModel>();
            _username = username;
            _password = password;
            ClientUsername = username;
            Debug.WriteLine($"{_username}|{_password}");
            string logInfo = $"{_username}|{_password}";
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.disconnectedEvent += UserDisconnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.IDReceivedEvent += IDReceived;
            _server.LoginConnectToServer(logInfo);
            SendMessage = new SendMessage(_server);
            NavigateLoginViewCommand = new NavigateLoginViewCommand(navigationStore);

        }

        public ClientViewModel(string username, string email, string password)
        {
            _username = username;
            _email = email;
            _password = password;
            userMessages = new ObservableCollection<MessageModel>();
            users = new ObservableCollection<UserModel>();
            Debug.WriteLine($"{_username}|{_email}|{_password}");
            string regInfo = $"{_username}|{_email}|{_password}";
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.disconnectedEvent += UserDisconnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.IDReceivedEvent += IDReceived;
            _server.RegisterConnectToServer(regInfo);
            SendMessage = new SendMessage(_server);
        }

        private void IDReceived()
        {
            UID = _server._packetReader.readMessage();
            if (UID == "No ID")
            {
                NavigateLoginViewCommand.Execute(null);
            }
            Debug.WriteLine($"User UID: {UID}");
        }

        private void UserConnected()
        {
            var user = new UserModel
            {
                Username = _server._packetReader.readMessage(),
            };

            if (!users.Any(x => x.Username == user.Username))
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
            var messageInfo = new MessageModel
            {
                messageUsername = _server._packetReader.readMessage(),
                messageContent = _server._packetReader.readMessage(),
                messageWidth = 0.0,
                messageHeight = 0.0

            };
            Debug.WriteLine($"Message received from: {messageInfo.messageUsername}, content: {messageInfo.messageContent}");
            messageInfo.messageHeight = GetMessageHeight(messageInfo.messageContent);
            messageInfo.messageWidth = GetMessageWidth(messageInfo.messageContent);
            Application.Current.Dispatcher.Invoke(() => userMessages.Add(messageInfo));

        }
        private double GetMessageWidth(string content)
        {
            if(content.Length >= 23)
            {
                return 300.0;
            }
            return 13.0 * content.Length;
        }
        private double GetMessageHeight(string content)
        {
            if(content.Length <= 20)
            {
                return 40.0;
            }
            return 40 * 2;
        }
    }

    

}
