using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.API.Models;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestGetByIdTruck
    {
        [Theory]
        [AutoDomainData]
        public async Task GetTruckById_OnSucess_ReturnsGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            Truck truck)
        {
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
                TruckFixture._mapper);

            var result = await sut.GetById<GetTruckModel>(truck.Id);

            result.Should().BeOfType<GetTruckModel>();
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTruckById_OnSucess_TruckIdMustBeTheSame(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            [Range(1, 9999)] int truckId)
        {
            var selectedTruck = TruckFixture.GenerateTruck(truckId);
            mockTruckRepository
                .Setup(repo => repo.Select(truckId))
                .ReturnsAsync(selectedTruck);
            var sut = new TruckService(
                mockTruckRepository.Object,
                TruckFixture._mapper);

            var result = await sut.GetById<GetTruckModel>(truckId);

            result.Id.Should().Be(truckId);
        }
    }
}
