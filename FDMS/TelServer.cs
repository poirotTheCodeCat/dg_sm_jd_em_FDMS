using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace FDMS
{
    class TelServer
    {
        private Thread listenerThread;
        private TcpListener server;
        private bool isConnected;

        public TelServer()
        {

        }

        public void StartListening()
        {
            Int32 port = 15000;
            IPAddress localIP = IPAddress.Parse("127.0.0.1");

            server = null;
            IPEndPoint clientIP;    // stores the client IP address

            try
            {
                server = new TcpListener(localIP, port);
                server.Start();

                // start a thread to listen for new clients
                ParameterizedThreadStart ts = new ParameterizedThreadStart(waitForClient);
                listenerThread = new Thread(ts);

                listenerThread.Start();
            }
            catch
            {
                server.Stop();
                throw new Exception("Something went wrong connecting to a client");
            }
        }

        private void waitForClient(object o)
        {
            TcpClient client = server.AcceptTcpClient();
            isConnected = true;
            try
            {
                waitForMessage(client);
            }
            catch
            {
                throw new Exception("Something went wrong reading client data");
            }
            server.Stop();
        }


        private void waitForMessage(object o)
        {
            TcpClient client = (TcpClient)o;
            Byte[] bytes = new byte[256];       // bytes will be used to read data
            String data = null;                 // this string will be used to read data


            NetworkStream sendStream;
            NetworkStream stream = client.GetStream();      // used to recieve message
            int i;


            while (((i = stream.Read(bytes, 0, bytes.Length)) != 0) && isConnected) // iterate through read stream
            {
                // convert data from byte to json

            }
            client.Close(); // shut down connection when user disconnects
        }

        public void stopListening()
        {
            isConnected = false;  // should cause the waitForMessage function to exit main loop, exit, then close the server
            listenerThread.Abort();
            server.Stop();
        }
    }
}
