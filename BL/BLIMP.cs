using System;
using BlApi;
using APIDL;
//using DL;
using BO;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BL
{
    public class BlImp1 : IBL
    {
        IDAL dl = DalFactory.GetDal();

        #region BusLineBO
        BO.BusLineBO BusLineDOBOAdapter(DO.BusLine busLineDo)
        {
            BO.BusLineBO busLineBO = new BO.BusLineBO();
            int BusLineKeyOfDO = busLineDo.BusLineKey;
            try
            {
                busLineDo = dl.GetBusLine(BusLineKeyOfDO);
            }
            catch(DO.BadBusLineKeyException ex)
            {
                throw new BO.BadBusLineKeyException("bus line key does not exist", ex);
            }
            busLineDo.CopyPropertiesTo(busLineBO);
            busLineBO.busLineStations =from sic in dl.get
            return busLineBO;
        }
        public bool HasBusStation(BusLineBO busLine, int stationKey)
        {
            if(busLine.busLineStations.FirstOrDefault(b => (b.BusLineStationKey == stationKey & b.IsActive)) !=null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region BusLineStationBO
        BO.BusLineStationBO BusLineStationDOBOAdapter(DO.BusLineStation busLineStationDo)
        {
            BO.BusLineStationBO busLineStationBO = new BO.BusLineStationBO();
            busLineStationDo.CopyPropertiesTo(busLineStationBO);
            return busLineStationBO;
        }
        public void AddStation(BusLineBO busLine, int stationKey)//im not finish this one because im not sure how to do it
        {
            DO.BusStation busStationDO = dl.GetBusStation(stationKey);
            BO.StationBO stationBO = BusStationDOBOAdapter(busStationDO);
        }
        public void DeleatStation(BusLineBO busLine, int stationKey)
        {
            busLine.busLineStations.FirstOrDefault(b => (b.BusLineStationKey == stationKey & b.IsActive)).IsActive = false;
        }
        public IEnumerable<BO.BusLineStationBO> GetAllBusLineStationOfBusLine(BusLineBO busLine)
        {
            return busLine.busLineStations;
        }
        #endregion

        #region StationBO
        BO.StationBO BusStationDOBOAdapter(DO.BusStation busStationDo)
        {
            BO.StationBO busStationBO = new BO.StationBO();
            busStationDo.CopyPropertiesTo(busStationBO);
            return busStationBO;
        }
        #endregion

        #region UserBO
        BO.User UserDOBOAdapter(DO.User user)
        {
            BO.User userBO = new BO.User();
            user.CopyPropertiesTo(userBO);
            return userBO;
        }
        #endregion
    }
}