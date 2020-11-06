using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FDMS
{
    public static class TelProcess
    {
        public static int packetNum = 0;
     
        public static Telemetry process(byte[] packetBytes, ITelemetry _db)
        {
            Packet packet = JsonSerializer.Deserialize<Packet>(packetBytes);
            return process(packet, _db);
        }

        public static Telemetry process(Packet packet, ITelemetry _db)
        {
            // Load the telemetry data from the packet 
            Telemetry tel = packet.TelemetryData;

            // check the checksum
            if(packet.CheckSum != Packet.calculateCheckSum(tel))    // if checksum fails
            {
                return null;
            }
            else
            {
                // call method to add to the database 
                
                InsertTelemetry(tel, _db);
                return tel;
            }
        }

        private static async Task InsertTelemetry(Telemetry t, ITelemetry _db)
        {
            await _db.InsertTelemetry(t);
        }

        // Method:      sendPackets()
        // Description: goes through each line in the file and sends the aircraft data to the connected ground terminal
        // Parameters:  string filename: the filename of the file that will have its contents read and sent to via packets
        //              NetworkStream stream: the stream to the server
        // Returns:     int: the packet sequence number of the next packet to be sent
        public static int sendPackets(string filename, ITelemetry _db)
        {
            

            //numOfPackets = sendPackets("C:\\tmp\\C-GEFC.txt", stream, numOfPackets);
            //numOfPackets = sendPackets("C:\\tmp\\C-QWWT.txt", stream, numOfPackets);
            byte[] bytes = new Byte[1024];
            string recievedPacketString = "";
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

                    string JSONPacket = JsonSerializer.Serialize(packet); 

           
                    // sends the data to the connected server
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(JSONPacket);
                    process(data, _db);

                    packetNum++;
                }
            }

            return packetNum;
        }
    }
}
