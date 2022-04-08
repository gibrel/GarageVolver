using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using GarageVolver.UnitTest.Helpers;
using System;

namespace GarageVolver.UnitTest.Fixtures
{
    public static class TruckFixture
    {
        public static readonly Random _random = new();

        public static Truck GenerateTruck()
        {
            return new Truck()
            {
                Id = 0,
                Model = TruckModelHelper.RandomTruckModel(),
                ManufacturingYear = DateTime.Now.Year,
                ModelYear = _random.Next(2) + DateTime.Now.Year,
            };
        }

        public static Truck MakeChanges(Truck truck)
        {
            int seed = _random.Next(3);

            switch (seed)
            {
                case 0:
                    ChangeModel(truck);
                    break;
                case 1:
                    ChangeModelYear(truck);
                    break;
                default:
                    ChangeModel(truck);
                    ChangeModelYear(truck);
                    break;
            }

            return truck;
        }

        private static void ChangeModel(Truck truck)
        {
            if (truck.Model.Name.Equals("FH")) truck.Model = Enumeration.GetByName<TruckModel>("FM");
            else if (truck.Model.Name.Equals("FM")) truck.Model = Enumeration.GetByName<TruckModel>("FH");
        }

        private static void ChangeModelYear(Truck truck)
        {
            if (truck.ModelYear.Equals(DateTime.Now.Year + 1)) truck.ModelYear -= 1;
            else truck.ModelYear += 1;
        }
    }
}
