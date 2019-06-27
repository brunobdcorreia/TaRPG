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
using RPGproject.Source.UserData;
using RPGproject.Source.CharacterCreation;

namespace RPGproject.Source.Connection
{
    class Server
    {
        private static UdpClient server = new UdpClient();
        private static IPEndPoint endP = new IPEndPoint(IPAddress.Any, 18763);

        public static void InitializeServer()
        {
            server.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            server.Client.Bind(endP);
            Thread ex = new Thread(run);
            ex.Start();
        }

        private static void run()
        {
            try
            {
                while (true)
                {
                    Debug.WriteLine("Waiting for broadcast");
                    byte[] bytes = server.Receive(ref endP);
                    Debug.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                    string [] com = Encoding.ASCII.GetString(bytes, 0, bytes.Length).Split(' ');
                    if (com[0].Equals("EDITCHARACTER")) {
                        Character x = new Character();
                        x.Name= com[1];
                        CharacterDB.DeleteCharacter(x);
                    }
                   else CharacterDB.executeCommand(Encoding.ASCII.GetString(bytes, 0, bytes.Length));

                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                server.Close();
            }
        }

    }
}
