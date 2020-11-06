using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FDMS
{
    public static class TelProcess
    {
        public static Telemetry process(byte[] packetBytes)
        {
            Packet packet = JsonSerializer.Deserialize<Packet>(packetBytes);
            return process(packet);
        }

        public static Telemetry process(Packet packet)
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
                return tel;
            }
        }
    }
}
