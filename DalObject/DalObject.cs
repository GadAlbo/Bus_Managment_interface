
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIDL;
using DO;
using DS;

namespace DL
{
    sealed class DalObject : IDAL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use
        #endregion

        #region BusLine
        public IEnumerable<BusLine> GetAllBusLines()
        {
            return from BusLine in DataSource.BusLineList
                   select BusLine.Clone();
        }
        public IEnumerable<BusLine> GetAllBusLinesBy(Predicate<BusLine> predicate)
        {
            return from BusLine in DataSource.BusLineList
                   where predicate(BusLine)
                   select BusLine.Clone();
        }
        public void AddBusLine(BusLine bus)
        {
            if (DataSource.BusLineList.FirstOrDefault(b => b.BusLineKey == bus.BusLineKey) != null)
                throw new BadBusLineKeyException(bus.BusLineKey, "Duplicate Bus Key");
            DataSource.BusLineList.Add(bus.Clone());
        }
        public void UpdateBusLine(BusLine bus)
        {
            throw new NotImplementedException();
        }
        public void UpdateBusLine(int busLineKey, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }
        public void DeleteBusLine(int busLineKey)
        {
            BusLine busLine= DataSource.BusLineList.Find(b => b.BusLineKey == busLineKey);
            if (busLine != null)
            {
                busLine.IsActive = false;
            }
            else
            {
                throw new BadBusLineKeyException(busLineKey, $"bad bus line key: {busLineKey}");
            }
        }
        public BusLine GetBusLine(int busLineKey)
        {
            BusLine busLine = DataSource.BusLineList.Find(b => b.BusLineKey == busLineKey);
            if (busLine != null)
                return busLine.Clone();
            else
                throw new BadBusLineKeyException(busLineKey, $"bad bus line key: {busLineKey}");
        }
        #endregion

        #region DrivingLine
        public void addLineInTravel(DrivingLine bus)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DrivingLine> GetBusLinetInDriveList(Predicate<DrivingLine> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region BusStation
        public BusStation GetBusStation(int busStationKey)
        {
            BusStation busStation = DataSource.BusStationList.Find(b => b.BusStationKey == busStationKey);
            if (busStation != null)
                return busStation.Clone();
            else
                throw new BadBusStationKeyException(busStationKey, $"bad bus line key: {busStationKey}");
        }
        #endregion

        #region User
        public User GetUser(string userName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus
        public bool addBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public bool update(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void delete(Bus bus)
        {
            throw new NotImplementedException();
        }

        public Bus read(int license)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}