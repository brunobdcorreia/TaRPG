using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.IO;


namespace ANDROID_TARPG.Source.Connection
{
    class Client
    {
        private static UdpClient client = new UdpClient();


        public static void SendData(string text)
        {
            client.Send(Encoding.ASCII.GetBytes(text), text.Length, "255.255.255.255", 18763);
            Debug.WriteLine("Sending : " + text);
        }

    }
}
