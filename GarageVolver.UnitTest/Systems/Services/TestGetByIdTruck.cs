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
    public class TestGetByIdTruck
    {
        private Mapper ConfigureMapper()
        {
            var truckMapProfile = new TruckMapProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(truckMapProfile));
            return new Mapper(configuration);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTruckById_OnSucess_ReturnsGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            Truck truck)
        {
            IMapper mapper = ConfigureMapper();
            GetTruckModel selectedTruck = new()
            {
                ModelName = truck.Model.Name,
                ModelYear = truck.ModelYear,
                ManufacturingYear = truck.ManufacturingYear
            };
            mockTruckRepository
                .Setup(repo => repo.Select(truck.Id))
                .ReturnsAsync(truck);
            var sut = new TruckService(
                mockTruckRepository.Object,
                mapper);

            var result = await sut.GetById<GetTruckModel>(truck.Id);

            result.Should().BeOfType<GetTruckModel>();
        }

    }
}
