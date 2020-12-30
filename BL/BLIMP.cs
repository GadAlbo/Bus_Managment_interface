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
        #region BusLineBO

        BO.BusLineBO BusLineDOBOAdapter(DO.BusLine busLineDo)
        {
            BO.BusLineBO busLineBO = new BO.BusLineBO();
            busLineDo.CopyPropertiesTo(busLineBO);
            return busLineBO;
        }

        #endregion
        #region BusLineStationBO
        BO.BusLineStationBO BusLineStationDOBOAdapter(DO.BusLineStation busLineStationDo)
        {
            BO.BusLineStationBO busLineStationBO = new BO.BusLineStationBO();
            busLineStationDo.CopyPropertiesTo(busLineStationBO);
            return busLineStationBO;
        }

        #endregion
    }
}