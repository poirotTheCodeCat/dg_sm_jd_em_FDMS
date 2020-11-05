using System;
using System.Collections.Generic;
using System.Text;

namespace FDMS
{
    // Consider possibly changing class name to AltitudeParams / AltitudeParameters so property can remain Altitude
    public class Altitude
    {
        private double altitude;
        private double pitch;
        private double bank;

        public Altitude()
        {

        }
        // Which default values would be best? 
        public Altitude(double altitude=0, double pitch=0, double bank=0)
        {
            AltitudeProperty = altitude;
            Pitch = pitch;
            Bank = bank;
        }
        public double AltitudeProperty
        {
            get { return altitude; }
            set { altitude = value; }
        }
        public double Pitch
        {
            get { return pitch; }
            set { pitch = value; }
        }
        public double Bank
        {
            get { return bank; }
            set { bank = value; }
        }
    }
}
