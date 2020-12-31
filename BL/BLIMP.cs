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
            catch (DO.BadBusLineKeyException ex)
            {
                throw new BO.BadBusLineKeyException("bus line key does not exist", ex);
            }
            busLineDo.CopyPropertiesTo(busLineBO);
            busLineBO.busLineStations = from b in dl.GetAllBusLineStationBy(b => (b.BusLineKey == BusLineKeyOfDO & b.IsActive))
                                        let ConsecutiveStations = dl.GetConsecutiveStations(b.StationNumberInLine - 1, b.StationNumberInLine)
                                        select ConsecutiveStations.CopyToBusLineStationBO(b);
            return busLineBO;
        }
        DO.BusLine BusLineBODOAdapter(BO.BusLineBO busLineBO)
        {
            DO.BusLine busLineDO = new BusLine();
            busLineBO.CopyPropertiesTo(busLineDO);
            return busLineDO;
        }
        public bool HasBusStation(BusLineBO busLine, int stationKey)
        {
            if (busLine.busLineStations.FirstOrDefault(b => (b.BusLineStationKey == stationKey & b.IsActive)) != null)
            {
                return true;
            }
            return false;
        }
        public BusLineBO GetBusLine(int busLineKey)
        {
            return BusLineDOBOAdapter(dl.GetBusLine(busLineKey));

        }
        public IEnumerable<BusLineBO> GetAllBusLines()
        {
           return from b in dl.GetAllBusLinesBy(b => b.IsActive)
                    select BusLineDOBOAdapter(b);
        }
        public IEnumerable<BusLineBO> GetAllBusLinesBy(Predicate<BusLineBO> predicate)
        {
            return from b in GetAllBusLines()
                where predicate(b)
                select b;
        }
        public void UpdateBusLine(BusLineBO busLine)
        {
            try
            {
                BusLineBO busLineBO = GetBusLine(busLine.BusLineKey);
                if (busLineBO != null)
                {
                    DeleteBusLine(busLine.BusLineKey);
                    AddBusLine(busLine);
                }
            }
            catch(DO.BadBusLineKeyException busExaption)
            {
                throw new BO.BadBusLineKeyException("this bus does not exsist", busExaption);
            }
        }
        public void AddBusLine(BusLineBO bus)
        {
            try
            {
                dl.AddBusLine(BusLineBODOAdapter(bus));
            }
            catch (DO.BadBusLineKeyException busExaption)
            {
                throw new BO.BadBusLineKeyException("this bus already exsist", busExaption);
            }
        }
        public void DeleteBusLine(int busLineKey)
        {
            try
            {
                dl.DeleteBusLine(busLineKey);
            }
            catch (DO.BadBusLineKeyException busExaption)
            {
                throw new BO.BadBusLineKeyException("this bus already exsist", busExaption);
            }
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
        public BO.StationBO BusStationDOBOAdapter(DO.BusStation busStationDo)
        {
            BO.StationBO busStationBO = new BO.StationBO();
            busStationDo.CopyPropertiesTo(busStationBO);
            busStationBO.busLines= from b in GetAllBusLines()
                                   where (b.busLineStations.FirstOrDefault
                                   (s => (s.BusLineStationKey == busStationDo.BusStationKey & s.IsActive)) != null)
                                   select b;
            return busStationBO;
        }
        DO.BusStation BusStationBODOAdapter(BO.StationBO busStationBO)
        {
            DO.BusStation busStationDO = new BusStation();
            busStationBO.CopyPropertiesTo(busStationDO);
            return busStationDO;
        }
        public StationBO GetBusStation(int busStationKey)
        {
            return BusStationDOBOAdapter(dl.GetBusStation(busStationKey));
        }
        public IEnumerable<StationBO> GetAllBusStations()
        {
            return from b in dl.GetAllBusStationsBy(b => b.IsActive)
                   select BusStationDOBOAdapter(b);
        }
    
        public IEnumerable<StationBO> GetAllBusStationsBy(Predicate<StationBO> predicate)
        {
            return from b in GetAllBusStations()
                   where predicate(b)
                   select b;
        }
        public void AddBusStation(StationBO station)
        {
            try
            {
                dl.AddBusStation(BusStationBODOAdapter(station));
            }
            catch (DO.BadBusStationKeyException busExaption)
            {
                throw new BO.BadBusStationKeyException("this bus already exsist", busExaption);
            }
        }
        public void DeleteBusStation(int busStationKey)
        {
            try
            {
                dl.DeleteBusStation(busStationKey);
            }
            catch (DO.BadBusStationKeyException busExaption)
            {
                throw new BO.BadBusStationKeyException("this bus does not exsist", busExaption);
            }
        }
        public void UpdateBusStation(StationBO station)
        {
            try
            {
                StationBO busStationBO = GetBusStation(station.BusStationKey);
                if (busStationBO != null)
                {
                    DeleteBusStation(station.BusStationKey);
                    AddBusStation(station);
                }
            }
            catch (DO.BadBusLineKeyException busExaption)
            {
                throw new BO.BadBusLineKeyException("this bus does not exsist", busExaption);
            }
        }
        public bool HasLine(StationBO station, int lineNumber)
        {
            if(station.busLines.FirstOrDefault(s => (s.BusLineKey == lineNumber & s.IsActive))!=null)
            {
                return true;
            }
            return false;
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