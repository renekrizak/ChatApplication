using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Net;

namespace ChatClient.Commands
{
    public class SendMessageCommand : CommandBase
    {
        private string UID { get; set; }
        public SendMessageCommand(string uid)
        {
            UID = uid;
        }
        public override void Execute(object parameter)
        {
            var conn = new Server();
            conn.SendMessageToServer($"{UID} has sent message: {(string)parameter}");

        }
    }
}
