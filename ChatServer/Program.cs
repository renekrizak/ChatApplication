using ChatServer.Net.IO;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {

        static List<Client> _users;
        static TcpListener _listener;
        static void Main()
        {
            //Queries.WriteUsers("test221", "username", "password", "email@email.com", DateTime.Now);
            //Queries.ReadUsers();
            Queries.ReadLastMessages();
            _users = new List<Client>();
            
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9551);
            _listener.Start();

            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);
                BroadcastConnection();
            }

            //notifies other users when new person connects
            static void BroadcastConnection()
            {
                foreach (var user in _users)
                {
                    foreach (var usr in _users)
                    {
                        var broadcastPacket = new PacketBuilder();
                        broadcastPacket.WriteOpCode(5);
                        if (usr.Username != null)
                        {
                            broadcastPacket.WriteString(usr.Username);
                            user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                        }
                        else
                        {
                            broadcastPacket.WriteString("Error");
                            user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                        }

                    }
                }
            }
            
        }
        public static void BroadcastMessage(string message, string username)
        {
            foreach (var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(4);
                msgPacket.WriteString(username);
                msgPacket.WriteString(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }

        public static void BroadcastDisconnect(string Username)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] User: [{Username}] has disconnected");
            var disconnectedUser = _users.Where(x => x.Username.ToString() == Username).FirstOrDefault();
            _users.Remove(disconnectedUser);
            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);
                broadcastPacket.WriteString(Username);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }
        }

        

    }
}

