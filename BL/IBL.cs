﻿using System;
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
        bool HasBusStation(BusLineBO busLine, int stationKey);//done
        void UpdateBusLine(BusLineBO busLine);
        void AddBusLine(BusLineBO bus);
        void DeleteBusLine(int busLineKey);
        #endregion

        #region BusLineStationBO
        IEnumerable<BO.BusLineStationBO> GetAllBusLineStationOfBusLine(BusLineBO busLine);//done
        DateTime DistanceBetweanStations(BusLineBO busLine,int firstStationKey, int lastStationKey);
        void AddStation(BusLineBO busLine,int stationKey);
        void DeleatStation(BusLineBO busLine,int stationKey);//done
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
        void AddDeatinationStation(int stationKey, Driving driving);
        void AddSourceStation(int stationKey, Driving driving);
        #endregion
        #region User
        #endregion

    }
}