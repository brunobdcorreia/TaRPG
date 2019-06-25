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
        private static byte[] buffer = new Byte[1024];
        private static List<Socket> clientSockets = new List<Socket>();
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        public static void InitializeServer()
        {
            SetupServer();
        }

        private static void SetupServer()
        {
            Debug.WriteLine("Setting up server");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 100));
            serverSocket.Listen(5);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Debug.WriteLine("Client connected");

            Socket socket = serverSocket.EndAccept(AR);
            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

        }

        private static void ReceiveCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] dataBuff = new byte[received];
            Array.Copy(buffer, dataBuff, received);

            string text = Encoding.ASCII.GetString(dataBuff);
            Debug.WriteLine("Text received: " + text);

            if (text.ToLower() == "get time")
            {
                sendData("Request", socket);
            }
            else sendData("Invalid request", socket);

        }

        private static void sendData(string text, Socket socket)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), socket);
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);

        }

        private static void SendCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}
