using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// is a DO.BusLineStation +DO.ConsecutiveStation
    /// </summary>
    public class BusLineStationBO
    {
        public int BusLineStationCode { get; set; }// i think it is need to be the key of the bus station
        public int StationNumberInLine { get; set; }
        public double DistanceFromLastStation { get; set; }
        public TimeSpan DriveDistanceTimeFromLastStation { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
