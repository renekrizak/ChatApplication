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
        public ICommand NavigateRegisterViewCommand { get; }
        public ICommand UploadProfilePictureCommand { get; }
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

        public string ProfilePicturePath { get; set; }

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
            _server.IDReceivedEvent += IDReceivedLogin;
            _server.LoginConnectToServer(logInfo);
            SendMessage = new SendMessage(_server);
            NavigateLoginViewCommand = new NavigateLoginViewCommand(navigationStore);
            UploadProfilePictureCommand = new UploadProfilePictureCommand();
        }

        public ClientViewModel(string username, string email, string password, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
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
            _server.IDReceivedEvent += IDReceivedRegister;
            _server.RegisterConnectToServer(regInfo);
            SendMessage = new SendMessage(_server);
            NavigateRegisterViewCommand = new NavigateRegisterViewCommand(navigationStore);
        }

        private void IDReceivedLogin()
        {
            UID = _server._packetReader.readMessage();
            if (UID == "No ID")
            {
                NavigateLoginViewCommand.Execute(null);
            }
            Debug.WriteLine($"User UID: {UID}");
        }
        private void IDReceivedRegister()
        {
            UID = _server._packetReader.readMessage();
            if (UID == "No ID")
            {
                NavigateRegisterViewCommand.Execute(null);
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
                messageHeight = 0.0,
                messageAlignment = "",
                messageBorderBackground = "",
                messageTextForeGround = ""

            };
            
            messageInfo.messageHeight = SetMessageHeight(messageInfo.messageContent);
            messageInfo.messageWidth = SetMessageWidth(messageInfo.messageContent);
            messageInfo.messageAlignment = SetMessageAlignment(messageInfo.messageUsername);
            messageInfo.messageTextForeGround = SetMessageTextForeground(messageInfo.messageUsername);
            messageInfo.messageBorderBackground = SetMessageBorderBackground(messageInfo.messageUsername);
            Debug.WriteLine($"Message received from: {messageInfo.messageUsername}, content: {messageInfo.messageContent}");
            Application.Current.Dispatcher.Invoke(() => userMessages.Add(messageInfo));

        }

        private string SetMessageBorderBackground(string messageUser)
        {
            if(messageUser == ClientUsername)
            {
                return "#615EF0";
            }
            return "#cccbca";
        }

        private string SetMessageTextForeground(string messageUser)
        {
            if(messageUser == ClientUsername)
            {
                return "White";
            }
            return "Black";
        }

        private double SetMessageWidth(string content)
        {
            if(content.Length >= 23)
            {
                return 300.0;
            }
            return 13.0 * content.Length;
        }
        private double SetMessageHeight(string content)
        {
            if(content.Length <= 22)
            {
                return 35.0;
            }
            if(content.Length >= 23 && content.Length < 40)
            {
                return 35.0 * 2;
            }
            if (content.Length >= 40 && content.Length < 60)
            {
                return 30.0 * 3;
            }
            if(content.Length >= 60 && content.Length < 80)
            {
                return 28.0 * 4;
            }
            if(content.Length >= 80 && content.Length < 100)
            {
                return 32.0 * 5;
            }
            if(content.Length >= 100 && content.Length < 120)
            {
                return 30.0 * 6;
            }
            if(content.Length >= 120 && content.Length < 140)
            {
                return 32.0 * 7;
            }
            return 40.0;

        }

        private string SetMessageAlignment(string messageUser)
        {
            if(messageUser == ClientUsername)
            {
                return "Right";
            }
            return "Left";
        }
    }
}
