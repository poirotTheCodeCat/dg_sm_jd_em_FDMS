using System;

namespace FDMS
{
    public class Telemetry
    {
        private String tailNum;
        private GForce gForceParameters;
        private Altitude altitudeParameters;
        private DateTime timeStamp;

        public Telemetry()
        {

        }
        public Telemetry(String tailNum, GForce gForce, Altitude altitude, DateTime timeStamp)
        {
            TailNum = tailNum;
            GForceParameters = gForce;
            AltitudeParameters = altitude;
            TimeStamp = timeStamp;
        }
        public string TailNum
        {
            get { return tailNum; }
            set { tailNum = value; }
        }
        public GForce GForceParameters
        {
            get { return gForceParameters; }
            set { gForceParameters = value; }
        }
        public Altitude AltitudeParameters
        {
            get { return altitudeParameters; }
            set { altitudeParameters = value; }
        }
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
    }
}
