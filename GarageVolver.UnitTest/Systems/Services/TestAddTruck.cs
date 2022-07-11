using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GarageVolver.API.Configurations;
using GarageVolver.API.Models;
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
        private static Mapper ConfigureMapper()
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
            CreateTruckModel newTruck = new(
                modelName: Enumeration.GetById<TruckModel>(truckModelId).Name,
                modelYear: truckModelYear,
                manufacturingYear: DateTime.Now.Year);
            Truck insertTruck = new(
                model: Enumeration.GetByName<TruckModel>(newTruck.ModelName),
                modelYear: newTruck.ModelYear,
                manufacturingYear: newTruck.ManufacturingYear)
            {
                Id = 0
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
