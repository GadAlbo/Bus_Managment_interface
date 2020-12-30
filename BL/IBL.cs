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
        BusLineBO GetBusLine(int busLineKey);
        IEnumerable<BusLineBO> GetAllBusLines();
        IEnumerable<BusLineBO> GetAllBusLinesBy(Predicate<BusLineBO> predicate);
        void UpdateBusLine(BusLineBO busLine);
        void AddBusLine(BusLineBO bus);
        void DeleteBusLine(int busLineKey);
        #endregion

        #region BusLineStationBO
        IEnumerable<BO.BusLineStationBO> GetAllBusLineStationOfBusLine(BusLineBO busLine);
        bool HasBusStation(BusLineBO busLine,int stationKey);
        DateTime DistanceBetweanStations(BusLineBO busLine,int firstStationKey, int lastStationKey);
        void AddStation(BusLineBO busLine,int stationKey);
        void DeleatStation(BusLineBO busLine,int stationKey);
        #endregion

        #region StationBO
        StationBO GetBusStation(int busStationKey);
        IEnumerable<StationBO> GetAllBusStations();
        IEnumerable<StationBO> GetAllBusStationsBy(Predicate<StationBO> predicate);
        void AddBusStation(StationBO station);
        void UpdateBusStation(StationBO station);
        void DeleteBusStation(int busStationKey);
        bool HasLine(StationBO station,int lineNumber);
        IEnumerable<IGrouping<int, int>> GetBusLineGrouptByStation();
        #endregion

        #region Driving
        void AddDeatinationStation(int stationKey);
        void AddSourceStation(int stationKey);
        #endregion
        #region User
        #endregion

    }
}