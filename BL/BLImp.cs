using BL.BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace BL
{
    class BLImp
    {
        #region Bus
        IDal dl = DalFactory.GetDal();
        Bus busDoBoAdapter(DO.Bus busDO)
        {
            Bus busBO = new Bus();

            busDO.CopyPropertiesTo(busBO);

            return busBO;
        }

        public BO.Bus GetBus(int id)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"License number does not exist: {id}", id);
            }
            return busDoBoAdapter(busDO);
        }

        public IEnumerable<BO.Bus> GetAllBuses()
        {

            return from bus in dl.GetAllBuses()
                   select busDoBoAdapter(bus);
        }
        public IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate)
        {
            IEnumerable<DO.Bus> doBuses = dl.GetAllBusesBy((b) => predicate(busDoBoAdapter(b)));
            return doBuses.Select((b) => busDoBoAdapter(b));
        }
        public void AddBus(BO.Bus bus)
        {
            ValidateBusFiledsCheck(bus);
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            try
            {
                dl.AddBus(busDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BadIdException(ex, "Duplicate bus ID", bus.LicenseNum);
            }

        }
        public void ValidateBusFiledsCheck(BO.Bus bus)
        {
            DateTime tempDate2018 = new DateTime(1, 1, 2018);
            DateTime tempDate2021 = DateTime.Now;
            DateTime tempDate1985 = new DateTime(1, 1, 1985);
            if (bus.LicenseNum > 100000000 || bus.LicenseNum < 1000000)
                throw new BO.BadIdException(bus.LicenseNum, $"bad bus License number: {bus.LicenseNum}");
            if (bus.LicenseNum < 10000000 || bus.LicenseNum > 1000000)
                if (bus.FromDate > tempDate2018 || bus.FromDate < tempDate1985)
                    throw new BO.BadTimeException(bus.FromDate, $"The time does not match the license plate: {bus.FromDate}");
            if (bus.LicenseNum < 100000000 || bus.LicenseNum > 10000000)
                if (bus.FromDate < tempDate2018 || bus.FromDate > tempDate2021)
                    throw new BO.BadTimeException(bus.FromDate, $"The time does not match the license plate: {bus.FromDate}");
            if (bus.TotalTrip < 0)
                throw new BO.BadTotalTripException(bus.TotalTrip, $"The Total Trip is lower then 0: {bus.TotalTrip}");
            if (bus.FuelRemain < 0 || bus.FuelRemain > 1200)
                throw new BO.BadFuelException(bus.FuelRemain, $"Wrong input for Fuel: {bus.FuelRemain}");
            if (bus.KilometersSinceLastTreatment > bus.TotalTrip || bus.KilometersSinceLastTreatment < (bus.TotalTrip - 20000))
                throw new BO.BadKilometersException(bus.KilometersSinceLastTreatment, $"Wrong input for Fuel: {bus.KilometersSinceLastTreatment}");
            if (bus.KilometersAtLastRefueling > bus.TotalTrip || bus.KilometersAtLastRefueling < (bus.TotalTrip - 1200))
                throw new BO.BadKilometersException(bus.KilometersAtLastRefueling, $"Wrong input for Fuel: {bus.KilometersAtLastRefueling}");
        }
        public void UpdateBus(BO.Bus bus)//?
        {
            ValidateBusFiledsCheck(bus);
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            try
            {
                dl.UpdateBus(busDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"bad bus id: {busDO.LicenseNum}", busDO.LicenseNum);
            }

        }

        public void DeleteBus(int id)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"License number does not exist: {id}", id);
            }

        }
        #endregion

    }
}

