using System;
using System.Collections.Generic;
using System.Text;

namespace FDMS
{
    public class GForce
    {
        private double accel_x;
        private double accel_y;
        private double accel_z;
        private double weight;

        public GForce()
        {

        }
        public GForce(double x, double y, double z, double weight)
        {
            Accel_x = x;
            Accel_y = y;
            Accel_z = z;
            Weight = weight;
        }
        public double Accel_x
        {
            get { return accel_x; }
            set { accel_x = value; }
        }
        public double Accel_y
        {
            get { return accel_y; }
            set { accel_y = value; }
        }
        public double Accel_z
        {
            get { return accel_z; }
            set { accel_z = value; }
        }
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        
    }
}
