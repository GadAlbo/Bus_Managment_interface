using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLineStation
    {
        public int BusLineStationKey { get; set; }
        public int BusLineStationCode { get; set; }// i think it is need to be the key of the bus station
        public int StationNumberInLine { get; set; }
        public bool IsActive { get; set; }
    }
}
