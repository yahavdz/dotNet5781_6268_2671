using System;
using System.Collections.Generic;
using System.Text;
using BL.BO;

namespace BL.BlApi
{
    interface IBL
    {
        #region Bus
        Bus GetBus(int busId);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(int id);
        #endregion Bus

    }
}
