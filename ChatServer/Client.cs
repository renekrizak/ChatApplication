using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
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
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }
        PacketReader _packetReader;
        public Client(TcpClient client)
        {
            ClientSocket = client;
           // UID = Guid.NewGuid();
            _packetReader = new PacketReader(ClientSocket.GetStream());

            var opcode = _packetReader.ReadByte(); //add checks for opcode
            data = _packetReader.readMessage();

            if(opcode == 2)
            {
                RegisterClient(Guid.NewGuid(), RegisterUsername(data), RegisterEmail(data), RegisterPassword(data), DateTime.Today);   
            }
                
            Console.WriteLine($"[{DateTime.Now}]: Client has connected with the username: {data}");
        }

        public void RegisterClient(Guid UID, string username, string email, string password, DateTime date)
        {
            Queries.RegisterClientQuery(UID, username, email, password, date);
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
    }
}
