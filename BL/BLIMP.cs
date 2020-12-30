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
        public void AddDeatinationStation(int stationKey)
        {
            throw new NotImplementedException();
        }

        public void AddSourceStation(int stationKey)
        {
            throw new NotImplementedException();
        }

        public void AddStation(int stationKey)
        {
            throw new NotImplementedException();
        }

        public void DeleatStation(int stationKey)
        {
            throw new NotImplementedException();
        }

        public DateTime DistanceBetweanStations(int firstStationKey, int lastStationKey)
        {
            throw new NotImplementedException();
        }
        #region BusLineBO

        BO.BusLineBO BusLineDOBOAdapter(DO.BusLine busLineDo)
        {
            BO.BusLineBO busLineBO = new BO.BusLineBO();
            busLineDo.CopyPropertiesTo(busLineBO);
            return busLineBO;
        }

        #endregion
        public bool HasLine(int lineNumber)
        {
            throw new NotImplementedException();
        }
    }
}