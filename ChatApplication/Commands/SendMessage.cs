using ChatClient.Net;

namespace ChatClient.Commands
{
    public class SendMessage : CommandBase
    {

        private readonly Server _server;

        public SendMessage(Server server)
        {
            _server = server;
        }
        public override void Execute(object parameter)
        {


            _server.SendMessageToServer((string)parameter);

        }
    }
}
