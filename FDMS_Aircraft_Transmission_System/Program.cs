using System;
using System.Linq;
using System.Net.Sockets;

namespace FDMS_Aircraft_Transmission_System
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverName = "127.0.0.1";
            int port = 13000;

            TcpClient client = new TcpClient(serverName, port);
            NetworkStream stream = client.GetStream();

            sendPackets("C:\\tmp\\C-FGAX.txt", client, stream);
            sendPackets("C:\\tmp\\C-GEFC.txt", client, stream);
            sendPackets("C:\\tmp\\C-QWWT.txt", client, stream);

            client.Close();
            stream.Close();
        }



        // Method:      sendPackets()
        // Description: 
        // Parameters:  string filename: the filename of the file that will have its contents read and sent to via packets
        //              TcpClient client:
        //              NetworkStream stream:
        // Returns:     N/A
        static void sendPackets(string filename, TcpClient client, NetworkStream stream)
        {
            string[] aircraftData = null;

            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                if (!line.Equals(lines.Last()))
                {
                    aircraftData = line.Split(',');
                }
            }
        }



    }
}
