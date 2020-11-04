using System;

namespace FDMS
{
    public class Telemetry
    {
        private String tailNum { get; set; }
        private GForce GforceParameters { get; set; }
        private Altitude AltitudeParameters { get; set; }
        private DateTime timeStamp { get; set; }
    }
}
