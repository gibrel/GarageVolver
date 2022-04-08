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
    public class TestAddTruck
    {
        [Theory]
        [AutoDomainData]
        public async Task AddTruck_OnSucess_ReturnsGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository)
        {
            var newTruck = TruckFixture.GenerateCreateTruckModel();
            var insertTruck = TruckFixture.TranslateTruckBetweenModels<CreateTruckModel, Truck>(newTruck);
            mockTruckRepository
                .Setup(repo => repo.Insert(insertTruck))
                .ReturnsAsync(true);
            var sut = new TruckService(
                mockTruckRepository.Object,
                TruckFixture._mapper);

            var result = await sut.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck);

            result.Should().BeOfType<GetTruckModel>();
        }
    }
}
