using System;

namespace FDMS
{
    public class Telemetry
    {
        String tailNum { get; set; }
        GForce GforceParameters { get; set; }
        Altitude AltitudeParameters { get; set; }
        DateTime timeStamp { get; set; }
    }
}
