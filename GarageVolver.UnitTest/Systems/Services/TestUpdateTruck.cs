using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.API.Models;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using GarageVolver.Service.Validators;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestUpdateTruck
    {
        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnSucess_ReturnsGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository)
        {
            var updateTruck = TruckFixture.GenerateUpdateTruckModel();
            var truck = TruckFixture.TranslateTruckBetweenModels<UpdateTruckModel, Truck>(updateTruck);
            mockTruckRepository
                .Setup(repo => repo.Update(truck))
                .ReturnsAsync(true);
            var sut = new TruckService(
                mockTruckRepository.Object,
                TruckFixture._mapper);

            var result = await sut.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(updateTruck);

            result.Should().BeOfType<GetTruckModel>();
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnSucess_ReturnsGetTruckModelWithSameId(
            [Frozen] Mock<ITruckRepository> mockTruckRepository)
        {
            var updateTruck = TruckFixture.GenerateUpdateTruckModel();
            var truck = TruckFixture.TranslateTruckBetweenModels<UpdateTruckModel, Truck>(updateTruck);
            mockTruckRepository
                .Setup(repo => repo.Update(truck))
                .ReturnsAsync(true);
            var sut = new TruckService(
                mockTruckRepository.Object,
                TruckFixture._mapper);

            var result = await sut.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(updateTruck);

            result.Id.Should().Be(updateTruck.Id);
        }
    }
}
