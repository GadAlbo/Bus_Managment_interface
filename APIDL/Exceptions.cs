using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BadBusLineKeyException : Exception
    {
        public int BUSLINEKEY;
        public BadBusLineKeyException(int busLineKey) : base() => BUSLINEKEY = busLineKey;
        public BadBusLineKeyException(int busLineKey, string message) :
            base(message) => BUSLINEKEY = busLineKey;
        public BadBusLineKeyException(int busLineKey, string message, Exception innerException) :
            base(message, innerException) => BUSLINEKEY = busLineKey;
        public override string ToString() => base.ToString() + $", bad line key: {BUSLINEKEY}";
    }
}
