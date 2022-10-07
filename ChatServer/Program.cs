using System;
using System.Net.Sockets;
using System.Net;

namespace ChatServer
{
    class Program
    {
        static TcpListener _listener;
        static void Main()
        {

            Console.WriteLine("Are you here cuh?");
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9551);
            _listener.Start();

            var client = _listener.AcceptTcpClient();
            
            Console.WriteLine("Client connected");

        }

    }
}

