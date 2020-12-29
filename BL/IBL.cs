using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IBL
    {
        #region Line
        bool HasBusStation(int stationKey);
        DateTime DistanceBetweanStations(int firstStationKey, int lastStationKey);
        void AddStation(int stationKey);
        void DeleatStation(int stationKey);

        #endregion

        #region Station
        bool HasLine(int lineNumber);
        #endregion

    }
}