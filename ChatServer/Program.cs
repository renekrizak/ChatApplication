using System;
using System.Net.Sockets;
using System.Net;

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

                /* breadcast conn to everyone*/
            }
           
            
            
            Console.WriteLine("Client connected");

        }

    }
}

