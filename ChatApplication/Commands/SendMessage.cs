using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Net;
using ChatClient.Net.IO;

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
