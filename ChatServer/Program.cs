using System;
using System.Net.Sockets;
using System.Net;
using ChatServer.Net.IO;

namespace ChatServer
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        static void Main()
        {

            _users = new List<Client>();
            Console.WriteLine("Are you here cuh?");
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
                        broadcastPacket.WriteString(usr.Username);
                        broadcastPacket.WriteString(usr.UID.ToString());
                        user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                    }
                }
            }
            Console.WriteLine("Client connected");

        }

    }
}

