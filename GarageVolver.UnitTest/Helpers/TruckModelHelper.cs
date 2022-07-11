using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using System;
using System.Linq;

namespace GarageVolver.UnitTest.Helpers
{
    public static class TruckModelHelper
    {
        public static readonly Random _random = new();

        public static TruckModel RandomTruckModel()
        {
            var listOfTruckModels = Enumeration.GetAll<TruckModel>().ToList();

            return listOfTruckModels[_random.Next(listOfTruckModels.Count)];
        }

        public static int RandomTruckModelId()
        {
            var listOfTruckModels = Enumeration.GetAll<TruckModel>().ToList();

            return _random.Next(listOfTruckModels.Count);
        }

        public static string RandomTruckModelName()
        {
            return RandomTruckModel().Name;
        }
    }
}
