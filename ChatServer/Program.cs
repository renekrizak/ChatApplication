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
            //Queries.WriteUsers("test221", "username", "password", "email@email.com", DateTime.Now);
            Queries.ReadUsers();
            _users = new List<Client>();
            
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9551);
            _listener.Start();

            while(true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);
                BroadcastConnection();
                SendID();
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
                        broadcastPacket.WriteOpCode(5);
                        if(usr.Username != null)
                        {
                            broadcastPacket.WriteString(usr.Username);
                            user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                        } else
                        {
                            broadcastPacket.WriteString("Error");
                            user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                        }
                        
                    }
                }
            }
            static void SendID()
            {
                var user = _users.Last();
                var idPacket = new PacketBuilder();
                string id = Queries.ReturnIDQuery(user.Username, user.Password);
                user.ID = id;
                idPacket.WriteOpCode(1);
                idPacket.WriteString(id);
                user.ClientSocket.Client.Send(idPacket.GetPacketBytes());
            }
     
        }
        public static void BroadcastMessage(string message)
        {
            foreach(var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(4); 
                msgPacket.WriteString(user.Username);
                msgPacket.WriteString(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }
        public static void BroadcastDisconnect(string Username)
        {
            var disconnectedUser = _users.Where(x => x.Username.ToString() == Username).FirstOrDefault();
            _users.Remove(disconnectedUser);
            foreach(var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);
                broadcastPacket.WriteString(Username);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }
        }

    }
}

