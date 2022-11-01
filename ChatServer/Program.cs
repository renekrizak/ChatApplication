using System;
using System.Net.Sockets;
using System.Net;
using ChatServer.Net.IO;
using Npgsql;

namespace ChatServer
{
    class Program
    {
        /*
         OP Codes:
         1-Login
         2-Register
         
         */
        static List<Client> _users;
        static TcpListener _listener;
        static void Main()
        {
            Queries.WriteUsers("test221", "username", "password", "email@email.com", DateTime.Now);
            Queries.ReadUsers();
            _users = new List<Client>();
            
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9551);
            _listener.Start();

            while(true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);

                /* broadcast conn to everyone*/
            }
           
            //notifies other users when new person connects
            static void BroadcastConnection()
            {
                foreach(var user in _users)
                {
                    foreach(var usr in _users)
                    {
                        var broadcastPacket = new PacketBuilder();
                        broadcastPacket.WriteOpCode(1);
                        broadcastPacket.WriteString(usr.data);
                        broadcastPacket.WriteString(usr.UID.ToString());
                        user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                    }
                }
            }
            Console.WriteLine("Client connected");

        }

    }
}

