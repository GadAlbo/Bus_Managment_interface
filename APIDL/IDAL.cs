﻿
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
        #region BusLine
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate);
        void AddBusLine(BusLine bus);
        void UpdateBusLine(BusLine bus);
        void UpdateBusLine(int busLineKey, Action<BusLine> update); //method that knows to updt specific fields in bus
        void DeleteBusLine(int busLineKey);
        BusLine GetBusLine(int busLineKey);
        #endregion

        #region BusStation
        BusStation GetBusStation(int busStationKey);
        IEnumerable<BusStation> GetAllBusStations();
        IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate);
        void AddBusStation(BusStation station);
        void UpdateBusStation(BusStation station);
        void DeleteBusStation(int busStationKey);
        #endregion

        #region BusLineStation
        IEnumerable<BusLineStation> GetAllBusLineStationBy(Predicate<BusLineStation> predicate);
        int GetBusLineStationKey(int BusLineKey, int StationNumberInLine);
        void AddBusLineStation(BusLineStation station);
        void UpdateBusLineStation(BusLineStation station);
        void DeleteBusLineStationInOneBusLine(int BusStationKey, int BusLineKey);
        void DeleteBusLineStationAllBusLine(int BusStationKey);

        #endregion

        #region User
        User GetUser(string userName);
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAlUersBy(Predicate<User> predicate);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeletUser(string userName);
        #endregion
        
        #region ConsecutiveStations
        ConsecutiveStations GetConsecutiveStations(int key1, int key2);
        IEnumerable<ConsecutiveStations> GetAllConsecutiveStations();
        IEnumerable<ConsecutiveStations> GetAlConsecutiveStationsBy(Predicate<ConsecutiveStations> predicate);
        void AddConsecutiveStations(ConsecutiveStations consecutiveStations);
        void UpdateConsecutiveStations(ConsecutiveStations consecutiveStations);
        void DeletConsecutiveStations(int key1, int key2);
        #endregion


    }
}