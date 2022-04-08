using AutoMapper;
using GarageVolver.API.Configurations;
using GarageVolver.API.Models;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using GarageVolver.UnitTest.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GarageVolver.UnitTest.Fixtures
{
    public static class TruckFixture
    {
        private static readonly Random _random = new();
        public static readonly IMapper _mapper = ConfigureMapper();

        public static Truck GenerateTruck(int id = 0)
        {
            return new Truck()
            {
                Id = id,
                Model = TruckModelHelper.RandomTruckModel(),
                ManufacturingYear = DateTime.Now.Year,
                ModelYear = _random.Next(2) + DateTime.Now.Year,
                LicencePlate = GenerateLicensePlate()
            };
        }

        public static List<Truck> GenerateListOfTrucks([Range(0, 10)] int ammount = 0, bool idNeeded = false)
        {
            var id = idNeeded ? _random.Next(1, 1000) : 0;
            var list = new List<Truck>();
            for (int i = 0; i < ammount; i++)
            {
                list.Add(GenerateTruck(idNeeded ? id++ : id));
            }
            return list;
        }

        public static CreateTruckModel GenerateCreateTruckModel()
        {
            return new CreateTruckModel()
            {
                ModelName = TruckModelHelper.RandomTruckModel().Name,
                ManufacturingYear = DateTime.Now.Year,
                ModelYear = _random.Next(2) + DateTime.Now.Year,
                LicencePlate = GenerateLicensePlate()
            };
        }

        public static List<GetTruckModel> GenerateListOfTGetruckModels([Range(0, 10)] int ammount = 0, bool idNeeded = false)
        {
            var id = idNeeded ? _random.Next(1, 1000) : 0;
            var list = new List<GetTruckModel>();
            for (int i = 0; i < ammount; i++)
            {
                list.Add(GenerateGetTruckModel(idNeeded ? id++ : id));
            }
            return list;
        }

        public static GetTruckModel GenerateGetTruckModel(int id = -8)
        {
            return new GetTruckModel()
            {
                Id = id < 0 ? _random.Next(int.MaxValue) : id,
                ModelName = TruckModelHelper.RandomTruckModel().Name,
                ManufacturingYear = DateTime.Now.Year,
                ModelYear = _random.Next(2) + DateTime.Now.Year,
                LicencePlate = GenerateLicensePlate()
            };
        }

        public static UpdateTruckModel GenerateUpdateTruckModel(int id = -8)
        {
            return new UpdateTruckModel()
            {
                Id = id < 0 ? _random.Next(int.MaxValue) : id,
                ModelName = TruckModelHelper.RandomTruckModel().Name,
                ManufacturingYear = DateTime.Now.Year,
                ModelYear = _random.Next(2) + DateTime.Now.Year,
                LicencePlate = GenerateLicensePlate()
            };
        }

        public static OutputModel TranslateTruckBetweenModels<InputModel, OutputModel>(
            InputModel modeledTruck) where InputModel : class where OutputModel : class
        {
            return _mapper.Map<InputModel, OutputModel>(modeledTruck);
        }

        public static Truck MakeChanges(Truck truck)
        {
            int seed = _random.Next(4);

            switch (seed)
            {
                case 0:
                    ChangeModel(truck);
                    break;
                case 1:
                    ChangeModelYear(truck);
                    break;
                case 2:
                    ChangeLicensePlate(truck);
                    break;
                default:
                    ChangeModel(truck);
                    ChangeModelYear(truck);
                    ChangeLicensePlate(truck);
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

        private static void ChangeLicensePlate(Truck truck)
        {
            truck.LicencePlate = GenerateLicensePlate(truck.LicencePlate);
        }

        public static string GenerateLicensePlate(string diferentThanThis = "")
        {
            var newAtempt = RandomString(3) + _random.Next(10).ToString() + RandomString(1) + _random.Next(100).ToString("00");
            newAtempt = newAtempt.Equals(diferentThanThis) ? GenerateLicensePlate(diferentThanThis) : newAtempt;
            return newAtempt;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private static Mapper ConfigureMapper()
        {
            var truckMapProfile = new TruckMapProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(truckMapProfile));
            return new Mapper(configuration);
        }
    }
}
