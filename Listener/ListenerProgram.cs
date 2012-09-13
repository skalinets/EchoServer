using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common;

namespace Listener
{
    class ListenerProgram
    {
        private const int ListernersCount = 100;

        static void Main(string[] args)
        {
            Console.Out.WriteLine("Starting Listener");
            var tasks = CreateListenerTasks();
            Task.WaitAll(tasks);
        }

        private static Task[] CreateListenerTasks()
        {
            return Enumerable.Range(1, ListernersCount)
                .Select(i => Task.Factory.StartNew(RegisterListener))
                .ToArray();
        }

        private static void RegisterListener()
        {
            var tcpClient = new TcpClient("127.0.0.1", 13001);
            TcpHelper.CreateListener(Process, tcpClient);
        }

        private static void Process(string data)
        {
            Console.Out.Write(data);
        }
    }
}
