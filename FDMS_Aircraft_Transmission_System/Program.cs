using FDMS;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace FDMS_Aircraft_Transmission_System
{
        class Program
    {
        static void Main(string[] args)
        {
            string serverName = "127.0.0.1";
            int port = 13000;
            int numOfPackets = 1;

            try
            {
                TcpClient client = new TcpClient();
                client.Connect(serverName, port);
                NetworkStream stream = client.GetStream();

                numOfPackets = sendPackets("C:\\tmp\\C-FGAX.txt", client, stream, numOfPackets);
                numOfPackets = sendPackets("C:\\tmp\\C-GEFC.txt", client, stream, numOfPackets);
                numOfPackets = sendPackets("C:\\tmp\\C-QWWT.txt", client, stream, numOfPackets);

                client.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        // Method:      sendPackets()
        // Description: 
        // Parameters:  string filename: the filename of the file that will have its contents read and sent to via packets
        //              TcpClient client:
        //              NetworkStream stream:
        // Returns:     N/A
        static int sendPackets(string filename, TcpClient client, NetworkStream stream, int packetNum)
        {
            // array of the aircraft data parsed into each attribute
            string[] aircraftData = null;
            // bool to keep track if the packet was successfully sent
            bool successfulPacket = false;

            // regex to match the aircraft tail from the filename
            Regex aircraftTailRegex = new Regex("(C-.*)[^.txt]");
            var aircraftMatch = aircraftTailRegex.Match(filename);
            // contains the aircraft tail 
            string aircraftTail = aircraftMatch.Groups[0].ToString();

            string[] lines = System.IO.File.ReadAllLines(filename);

            // goes through each line of the file, and sends the contents via packets to the ground terminal
            foreach (string line in lines)
            {
                successfulPacket = false;

                // checks that the current line being evaluated is the last/EOF, will then parse the aircraft data and send it
                if (!line.Equals(lines.Last()))
                {
                    aircraftData = line.Split(',');

                    Telemetry tData = new Telemetry(aircraftTail, Double.Parse(aircraftData[1]), Double.Parse(aircraftData[2]), Double.Parse(aircraftData[3]), Double.Parse(aircraftData[4]),
                        Double.Parse(aircraftData[5]), Double.Parse(aircraftData[6]), Double.Parse(aircraftData[7]), Convert.ToDateTime(aircraftData[0].Trim().Replace('_', '-')));
                    Packet packet = new Packet(tData, packetNum);

                    string JSONPacket = JsonConvert.SerializeObject(packet);

                    // loop to constantly send the packet until conformation that the sent packet has been successfully recieved
                    while (!successfulPacket)
                    {
                        // sends the data to the connected server
                        byte[] data = System.Text.Encoding.ASCII.GetBytes(JSONPacket);
                        stream.Write(data, 0, data.Length);

                        // wait for response

                        successfulPacket = true;
                    }

                    packetNum++;
                }
            }

            return packetNum;
        }
    }
}
