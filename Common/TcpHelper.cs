using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Common
{
    public class TcpHelper
    {
        public static async void CreateListener(Action<string> action, TcpClient tcpClient)
        {
            var networkStream = tcpClient.GetStream();
            var r = new StreamReader(networkStream);
            var buffer = new char[1];
            while (true)
            {
                if (r.EndOfStream)
                {
                    Task.Delay(500);
                    continue;
                }

                await r.ReadAsync(buffer, 0, buffer.Length);
                action(new string(buffer));
            }
        }

        public static async void StartListener(int port, Action<TcpClient> action)
        {
            Console.Out.WriteLine("added listener for {0}", port);
            var tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            tcpListener.Start();

            while (true)
            {
                var client = await tcpListener.AcceptTcpClientAsync();
                Console.Out.WriteLine("Server: client connected");
                action(client);
            }
        }
    }
}