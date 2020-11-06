using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace FDMS
{
    /*
     * Class: TelServer
     * Description: This class is used to listen for incoming messages from an aircraft
     * Upon recieving aircraft telemetry information over TCP/IP - this class calls methods from
     * external classes to process the recieved data and store it in the database
     */
    class TelServer
    {
        private Thread listenerThread;
        private TcpListener server;
        private bool isConnected;

        public TelServer()
        {

        }

        /*
         * Function: StartListening()
         * Description: begins a thread running in the background which listens for incoming packets 
         * from the aircraft transmission system
         */
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

        /*
         * Function: waitForClient()
         * Description: Runs in the background as a thread, waits for a client to connect
         */
        private void waitForClient(object o)
        {
            try
            {
                TcpClient client = server.AcceptTcpClient();    // wait for client to connect
                isConnected = true;
                waitForMessage(client);
            }
            catch
            {
                throw new Exception("Something went wrong reading client data");
            }
            server.Stop();
        }

        /*
         * Function: waitForMessage()
         * Description: After connecting to a client, this waits to recieve a message from the client
         */
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
                // call TelProcess.process

            }
            client.Close(); // shut down connection when user disconnects
        }

        /*
         * Function: stopListening()
         * Description: Shuts down the connection and terminates the background listening thread
         */
        public void stopListening()
        {
            isConnected = false;  // should cause the waitForMessage function to exit main loop, exit, then close the server
            server.Stop();
            listenerThread.Abort();
        }
    }
}
