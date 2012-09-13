using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using Common;

namespace Server
{
    internal class ServerProgram
    {
        private static void Main(string[] args)
        {
            Console.Out.WriteLine("starting server");
            new Server().Start();
            Console.Out.WriteLine("press enter to close");
            Console.ReadLine();
        }
    }

    public class Server
    {
        private readonly List<TcpClient> listeners = new List<TcpClient>();

        public void Start()
        {
            TcpHelper.StartListener(13000, RegisterSender);
            TcpHelper.StartListener(13001, client => listeners.Add(client));
        }

        private void RegisterSender(TcpClient client)
        {
            TcpHelper.CreateListener(NotifyAllListeners, client);
        }

        private void NotifyAllListeners(string message)
        {
            listeners.AsParallel().ForAll(listener => NotifyListener(message, listener));
        }

        private static void NotifyListener(string message, TcpClient listener)
        {
            var stream = listener.GetStream();
            using (var streamWriter = new StreamWriter(stream, Encoding.ASCII, 1, true))
            {
                streamWriter.WriteAsync(message);
            }
        }
    }
}