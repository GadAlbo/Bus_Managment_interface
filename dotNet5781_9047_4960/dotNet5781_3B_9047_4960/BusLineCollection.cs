using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet5781_01_9047_4960;

namespace dotNet5781_3B_9047_4960
{
    class BusLineCollection : IEnumerable
    {
        public List<dotNet5781_01_9047_4960.Program.Bus> buses = new List<dotNet5781_01_9047_4960.Program.Bus>();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return buses.GetEnumerator();
        }
        
    }
}
