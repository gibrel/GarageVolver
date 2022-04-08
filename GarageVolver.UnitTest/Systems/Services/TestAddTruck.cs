using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GarageVolver.API.Models;
using GarageVolver.API.Configurations;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using GarageVolver.Service.Validators;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestAddTruck
    {
        private Mapper ConfigureMapper()
        {
            var truckMapProfile = new TruckMapProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(truckMapProfile));
            return new Mapper(configuration);
        }

        [Theory]
        [AutoDomainData]
        public async Task AddTruck_OnSucess_ReturnsGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            [Range(0, 1)] int truckModelId,
            [Range(0, 1)] int truckModelYear)
        {
            IMapper mapper = ConfigureMapper();
            truckModelYear += DateTime.Now.Year;
            CreateTruckModel newTruck = new()
            {
                ModelName = Enumeration.GetById<TruckModel>(truckModelId).Name,
                ModelYear = truckModelYear,
                ManufacturingYear = DateTime.Now.Year
            };
            Truck insertTruck = new()
            {
                Id = 0,
                Model = Enumeration.GetByName<TruckModel>(newTruck.ModelName),
                ManufacturingYear = newTruck.ManufacturingYear,
                ModelYear = newTruck.ModelYear,
            };
            mockTruckRepository
                .Setup(repo => repo.Insert(insertTruck))
                .ReturnsAsync(true);
            var sut = new TruckService(
                mockTruckRepository.Object,
                mapper);

            var result = await sut.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck);

            result.Should().BeOfType<GetTruckModel>();
        }
    }
}
