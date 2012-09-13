using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    class SenderProgram
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Starting Sender");
            Console.Out.WriteLine("TYPE HERE");

            StartSender();
        }

        private static void StartSender()
        {
            var tcpClient = new TcpClient("127.0.0.1", 13000);

            ProcessKeys(tcpClient);
        }

        private static void ProcessKeys(TcpClient tcpClient)
        {
            while (true)
            {
                var c = ReadChar();
                if (Char.IsLetter(c))
                {
                    SendChar(tcpClient, c);
                }
            }
        }

        private static char ReadChar()
        {
            var consoleKeyInfo = Console.ReadKey();
            var c = consoleKeyInfo.KeyChar;
            return c;
        }

        private static void SendChar(TcpClient tcpClient, char c)
        {
            using (var streamWriter = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII, 1, true))
            {
                streamWriter.Write(c);
            }
        }
    }
}
