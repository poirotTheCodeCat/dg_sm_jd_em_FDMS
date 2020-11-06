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

            try
            {
                TcpClient client = new TcpClient();
                //client.Connect(serverName, port);
                NetworkStream stream = null;

                sendPackets("C:\\tmp\\C-FGAX.txt", client, stream);
                //sendPackets("C:\\tmp\\C-GEFC.txt", client, stream);
                //sendPackets("C:\\tmp\\C-QWWT.txt", client, stream);

                //client.Close();
                //stream.Close();
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
        static void sendPackets(string filename, TcpClient client, NetworkStream stream)
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
                    
                    //GForce gForceData = new GForce(Double.Parse(aircraftData[1]), Double.Parse(aircraftData[2]), Double.Parse(aircraftData[3]), Double.Parse(aircraftData[4]));
                    //Altitude altitudeData = new Altitude(Double.Parse(aircraftData[5]), Double.Parse(aircraftData[6]), Double.Parse(aircraftData[7]));
                    Telemetry tData = new Telemetry(aircraftTail, Double.Parse(aircraftData[1]), Double.Parse(aircraftData[2]), Double.Parse(aircraftData[3]), Double.Parse(aircraftData[4]),
                        Double.Parse(aircraftData[5]), Double.Parse(aircraftData[6]), Double.Parse(aircraftData[7]), Convert.ToDateTime(aircraftData[0].Trim().Replace('_', '-')));

                    // loop to constantly send the packet until conformation that the sent packet has been successfully recieved
                    while (!successfulPacket)
                    { 
                        // Create JSON string of telemetry data/packet
                        // send JSON string
                        // wait for response

                        successfulPacket = true;
                    }
                }
            }
        }
    }
}
