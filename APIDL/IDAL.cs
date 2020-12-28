
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
        void AddBusLine(BusLine bus);
        void UpdateBusLine(BusLine bus);
        void UpdateBusLine(int busLineKey, Action<BusLine> update); //method that knows to updt specific fields in bus
        void DeleteBusLine(int busLineKey);
        BusLine GetBusLine(int busLineKey);
        #endregion

        #region DrivingLine
        void addLineInTravel(DrivingLine bus);
        IEnumerable<DrivingLine> GetBusLinetInDriveList(Predicate<DrivingLine> predicate);
        #endregion

        #region BusStation
        BusStation GetBusStation(int busStationKey);
        IEnumerable<BusStation> GetAllBusStations();
        IEnumerable<BusStation> GetAllBusStationsBy(Predicate<BusStation> predicate);
        #endregion

        #region User
        User GetUser(string userName);
        #endregion

    }
}