using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebTelemetryFSUIPC
{
    class Server
    {

        Socket ServerSocket = null;
        int Port = 99;
        int BackLog = 5;
        MainPanel Context = null;
        List<Socket> Clients = new List<Socket>();
        Byte[] Buffer = new Byte[1024];

        public Server(MainPanel context)
        {
            Context = context;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
            ServerSocket.Listen(BackLog);
            Accept();
        }

        public void Accept()
        {
            ServerSocket.BeginAccept(new System.AsyncCallback(AcceptCallback), null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket client = ServerSocket.EndAccept(ar);
            Clients.Add(client);
            client.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);
            Accept();
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int size = client.EndReceive(ar);
            Byte[] RecievedBytes = new Byte[size];

            Array.Copy(Buffer, RecievedBytes, size);
            String data = Encoding.ASCII.GetString(RecievedBytes);
            Context.AddLog(data);

            String Response = "<h2>Hello World</h2>";
            Byte[] ResBytes = Encoding.ASCII.GetBytes(Response);
            client.BeginSend(ResBytes, 0, ResBytes.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {

        }
    }
}
