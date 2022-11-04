using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
     class Client
    {
        public string data { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ID { get; set; }
        public Guid UID { get; set; }
        
        public TcpClient ClientSocket { get; set; }
        PacketReader _packetReader;
        PacketBuilder _packetBuilder;
        public Client(TcpClient client)
        {
            ClientSocket = client;
           // UID = Guid.NewGuid();
            _packetReader = new PacketReader(ClientSocket.GetStream());

            var opcode = _packetReader.ReadByte(); //add checks for opcode
            data = _packetReader.readMessage();
            Username = LoginUsername(data);
            Password = LoginPassword(data);
            Task.Run(() => Process());
            
        }

        void Process()
        {
            while(true)
            {
                try
                {
                    Console.WriteLine($"Process for {Username} started");
                    var opcode = _packetReader.ReadByte();
                    switch(opcode)
                    {
                        case 1:
                            string username = LoginUsername(data);
                            string password = LoginPassword(data);
                           
                            break;
                        case 2:
                            username = RegisterUsername(data);
                            password = RegisterPassword(data);
                            string email = RegisterPassword(data);
                            Queries.RegisterClientQuery(Guid.NewGuid(), username, email, password, DateTime.Now);
                            string uid = Queries.ReturnIDQuery(username, password);
                            Console.WriteLine($"User with credentials: {data} registered and has UID: {uid}");
                            break;
                        case 3:
                            var msg = _packetReader.ReadString();
                            Console.WriteLine($"Message recieved: {msg}");
                            Program.BroadcastMessage(msg);
                            break;
                        default:
                            break;
                    }
                } catch(Exception)
                {
                    Console.WriteLine($"[{Username}] Disconnected");
                    Program.BroadcastDisconnect(Username.ToString());
                    ClientSocket.Close();
                    break;

                }
            }
        }

        private static void SendIDToClient(string id)
        {
            var idPacket = new PacketBuilder();
            idPacket.WriteOpCode(1);
            idPacket.WriteString(id);
        
        }

        /*
         Extracts username from the register packet
         */
        public string RegisterUsername(string data)
        {
            int count = 0;
            int i = 0;
            string username = "";
            while (count < 1)
            {
                if (data[i] == '|')
                {
                    count++;
                    return username;
                }
                username += data[i];
                i++;
            }
            return username;
        }
        /*
         Extracts email from the register packet
         */
        public string RegisterEmail(string data)
        {
            bool loopFlag = true;
            int lastPos = 0;
            int count = 0;
            string result = "";
            while(loopFlag)
            {
                if (data[lastPos] != '|' && count == 1)
                {
                    result += data[lastPos];
                }
                if(count == 2)
                {
                    return result;
                }
                if (data[lastPos] == '|')
                {
                    count++;
                }
                lastPos++;

            }
            return result;
        }

        public string RegisterPassword(string data)
        {
            bool loopFlag = true;
            int lastPos = 0;
            int count = 0;
            string result = "";
            while(loopFlag)
            {
                if(lastPos == data.Length)
                {
                    return result;
                }
                if (count == 2)
                {
                    result += data[lastPos];
                }
                if (data[lastPos] == '|')
                {
                    count++;
                }
                lastPos++;
            }
            return result;
        }

        public string LoginUsername(string data)
        {
            bool loopFlag = true;
            int lastPos = 0;
            int count = 0;
            string result = "";
            while(loopFlag)
            {
                if (data[lastPos] == '|')
                {
                    return result;
                }
                if (count == 0)
                {
                    result += data[lastPos];
                }
                lastPos++;
            }
            return "test1Meno";
            
        }
        public string LoginPassword(string data)
        {
            bool loopFlag = true;
            int lastPos = 0;
            int count = 0;
            string result = "";
            while (loopFlag)
            {
                
                if(lastPos == data.Length)
                {
                    return result;
                }
                if(count == 1)
                {
                    result += data[lastPos];
                }
                if (data[lastPos] == '|')
                {
                    count++;
                }
               
                lastPos++;

            }
            return result;
        }
    }
}
