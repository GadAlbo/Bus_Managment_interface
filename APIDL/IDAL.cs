
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace APIDL

{
    public interface IDAL
    {
        bool addBus(Bus bus);
        bool update(Bus bus);
        void delete(Bus bus);
        Bus read(int license);
        bool addBusInTravel(DrivingBus bus);

    }
}