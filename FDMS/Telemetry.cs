using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FDMS
{
    /*
     * Class: Telemetry
     * Description: Holds telemetry data about the GForce Parameters and Altitude Parameters
     * Data Members: 
     *  private String tailNum;
     *  private double accel_x;
     *  private double accel_y;
     *  private double accel_z;
     *  private double weight;
     *  private double altitude;
     *  private double pitch;
     *  private double bank;
     *  private DateTime timeStamp;
     */
    public class Telemetry : ITelemetry
    {

        private String tailNum;
        private double accel_x;
        private double accel_y;
        private double accel_z;
        private double weight;
        private double altitude;
        private double pitch;
        private double bank;
        private DateTime timeStamp;

        private readonly ISqlDataAccess _db;

        public Telemetry()
        {

        }
        public Telemetry(String tail, double x, double y, double z, double w, double a, double p, double b, DateTime ts)
        {
            TailNum = tail;
            Accel_x = x;
            Accel_y = y;
            Accel_z = z;
            Weight = w;
            Altitude = a;
            Pitch = p;
            TimeStamp = ts;
        }

        public Telemetry(Telemetry t)
        {
            TailNum = t.TailNum;
            Accel_x = t.Accel_x;
            Accel_y = t.Accel_y;
            Accel_z = t.Accel_z;
            Weight = t.Weight;
            Altitude = t.Altitude;
            Pitch = t.Pitch;
            TimeStamp = t.TimeStamp;
        }
        public Telemetry(ISqlDataAccess db)
        {
            _db = db;
        }

        public string TailNum
        {
            get { return tailNum; }
            set { tailNum = value; }
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

        public double Altitude
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
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }


        public Task<List<Telemetry>> GetTelemetry()
        {
            string sql = "select * from TelemetryData"; 
            return _db.LoadData<Telemetry, dynamic>(sql, new { });
        }

        public Task<List<Telemetry>> SearchTelemetry(string search)
        {
            string sql = @"select * from TelemetryData where TailNum = @search"; 
            return _db.LoadData<Telemetry, dynamic>(sql, new { search = search});
        }

        public Task InsertTelemetry(Telemetry telemetry)
        {
            string sql = @"EXEC sp_Insert @TailNum, @Accel_x, @Accel_y, @Accel_z, @Weight, @Altitude,
                @Pitch, @Bank, @TimeStamp";
            return _db.SaveData(sql, telemetry);
        }
    }
}
