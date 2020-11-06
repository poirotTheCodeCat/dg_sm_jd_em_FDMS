using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FDMS
{
    public interface ITelemetry
    {
        double Accel_x { get; set; }
        double Accel_y { get; set; }
        double Accel_z { get; set; }
        double Altitude { get; set; }
        double Bank { get; set; }
        double Pitch { get; set; }
        string TailNum { get; set; }
        DateTime TimeStamp { get; set; }
        double Weight { get; set; }

        Task<List<Telemetry>> GetTelemetry();
        Task InsertTelemetry(Telemetry telemetry);
    }
}