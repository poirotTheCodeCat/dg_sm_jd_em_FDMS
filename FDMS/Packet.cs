using System;
using System.Collections.Generic;
using System.Text;

namespace FDMS
{
    public class Packet
    {
        private Telemetry telemetryData;
        private int packetSequence;
        private int checksum;

        public Packet()
        {

        }

        public Packet(Telemetry initalTelemetry, int initalSequence)
        {
            telemetryData = initalTelemetry;
            packetSequence = initalSequence;
            checksum = calculateCheckSum();
        }

        public int CheckSum
        {
            get { return checksum; }
            set { checksum = value; }
        }

        public int PacketSequence
        {
            get { return packetSequence; }
            set { packetSequence = value; }
        }

        public Telemetry TelemetryData
        {
            get { return telemetryData; }
            set { telemetryData = value; }
        }

        public int calculateCheckSum()
        {
            double result = 0;

            result = (telemetryData.Altitude + telemetryData.Pitch + telemetryData.Bank) / 3;

            return (int)result;
        }
    }
}
