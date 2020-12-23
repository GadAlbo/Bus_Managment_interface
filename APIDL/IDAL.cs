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
        #region Bus //i think we need to erace it
        bool addBus(Bus bus);
        bool update(Bus bus);
        void delete(Bus bus);
        Bus read(int license);
        #endregion

        #region BusLine
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate);
        bool AddBusLine(BusLine bus);
        bool UpdateBusLine(BusLine bus);
        bool UpdateBusLine(int busLineKey, Action<BusLine> update); //method that knows to updt specific fields in Person
        void DeleteBusLine(BusLine bus);
        BusLine GetBusLine(int busLineKey);
        #endregion

        #region DrivingLine
        bool addLineInTravel(DrivingLine bus);
        IEnumerable<DrivingLine> GetBusLinetInDriveList(Predicate<DrivingLine> predicate);
        #endregion

        #region BusStation
        BusStation GetBusStation(int busStationKey);
        #endregion

        #region User
        User GetUser(string userName);
        #endregion

    }
}