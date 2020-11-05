using System;

namespace FDMS
{
    public class Telemetry
    {
        public String tailNum { get; set; }
        public GForce GforceParameters { get; set; }
        public Altitude AltitudeParameters { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
