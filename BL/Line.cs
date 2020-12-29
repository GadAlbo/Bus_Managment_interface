using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO
{
    public class Line
    {
        public int Key { get; set; }
        public BusLine ThisLine { get; set; }
        public IEnumerable<BusLineStation> StationsInLine { get; set; }
        public IEnumerable<ConsecutiveStations> ConsecutiveStationsInLine { get; set; }
    }
}
