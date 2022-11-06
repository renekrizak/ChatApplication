using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ChatClient.Net.IO;
using System.Diagnostics;
using ChatClient.ViewModel;

namespace ChatClient.Net
{
    public class Server
    {
        TcpClient _client;
        public PacketReader _packetReader;

        public event Action connectedEvent;
        public event Action msgReceivedEvent;
        public event Action disconnectedEvent;
        public event Action IDReceivedEvent;

        public Server()
        {
            _client = new TcpClient();
        }
        //maybe connect after you reach client view and only give access once you get UID?
        public void LoginConnectToServer(string loginInfo)
        {
            if(!_client.Connected)
            {
                _client.Connect("127.0.0.1", 9551);
                _packetReader = new PacketReader(_client.GetStream());
                if(!string.IsNullOrEmpty(loginInfo))
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(1);
                    connectPacket.WriteString(loginInfo);
                    _client.Client.Send(connectPacket.GetPacketBytes());
                }
                ReadPackets();

            }   
        }
        public void RegisterConnectToServer(string regInfo)
        {
            if(!_client.Connected)
            {
                _client.Connect("127.0.0.1", 9551);
                var connectPacket = new PacketBuilder();
                connectPacket.WriteOpCode(2);
                connectPacket.WriteString(regInfo);
                _client.Client.Send(connectPacket.GetPacketBytes());
            }
        }
        private void ReadPackets()
        {
            Task.Run(() =>
            {
                Debug.WriteLine("Started reading");
                while(true)
                {
                    var opcode = _packetReader.ReadByte();
                    switch(opcode)
                    {
                        case 1:
                            IDReceivedEvent?.Invoke();
                            break;
                        case 5:
                            Debug.WriteLine("Niekto sa connectol");
                            connectedEvent?.Invoke();
                            break;
                        case 4:
                            msgReceivedEvent?.Invoke();
                            break;
                        case 10:
                            disconnectedEvent?.Invoke();
                            break;
                        default:
                            
                            Console.WriteLine("idk");
                            break;
                    }  
                }
            });

        }
        public void SendMessageToServer(string message)
        {
            Debug.WriteLine($"Message: {message}");
            message += "000";
                var messagePacket = new PacketBuilder();
                messagePacket.WriteOpCode(3);
                messagePacket.WriteString(message);
                _client.Client.Send(messagePacket.GetPacketBytes());
        }
    }
}
