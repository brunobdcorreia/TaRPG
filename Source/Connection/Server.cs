using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace RPGproject.Source.Connection
{
    class Server
    {
        private static UdpClient server = new UdpClient(18763);
        private static IPEndPoint endP = new IPEndPoint(IPAddress.Any, 18763);

        public static void InitializeServer()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast    " + IPAddress.Any);
                    byte[] bytes = server.Receive(ref endP);
                    Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytes.Length));

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Close();
            }
        }


    }
}
