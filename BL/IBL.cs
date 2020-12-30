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
        #region BusLineBO
        bool HasBusStation(int stationKey);
        DateTime DistanceBetweanStations(int firstStationKey, int lastStationKey);
        void AddStation(int stationKey);
        void DeleatStation(int stationKey);

        #endregion

        #region StationBO
        bool HasLine(int lineNumber);
        #endregion

        #region Driving
        void AddDeatinationStation(int stationKey);
        void AddSourceStation(int stationKey);
        #endregion
        #region User
        #endregion

    }
}