using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ChatClient.Net.IO;

namespace ChatClient.Net
{
     class Server
    {
        TcpClient _client;
        public Server()
        {
            _client = new TcpClient();
        }

        public void ConnectToServer()
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 9551);
                var connectPacket = new PacketBuilder();
                connectPacket.WriteOpCode(0);
                connectPacket.WriteString("Username,password");
                _client.Client.Send(connectPacket.GetPacketBytes());
            }
        }
    }
}
