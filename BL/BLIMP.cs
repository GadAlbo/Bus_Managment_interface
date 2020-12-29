using System;
using BlApi;
using APIDL;
//using DL;
using BO;
using DO;

namespace BL
{
    public class BlImp1 : IBL
    {
        #region Line
        public bool HasBusStation(int stationKey)
        {
            BusLineStation lineStation = new BusLineStation();
            lineStation.BusLineStationKey = stationKey;

            return true;
        }
        #endregion
    }
}