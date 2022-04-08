using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestDeleteTruck
    {

        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_OnSucess_ReturnsTrue(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            [Range(1,9999)] int toBeDeletedTruckId)
        {
            mockTruckRepository
                .Setup(repo => repo.Delete(toBeDeletedTruckId))
                .ReturnsAsync(true);
            var sut = new TruckService(
                mockTruckRepository.Object,
                TruckFixture._mapper);

            var result = await sut.Delete(toBeDeletedTruckId);

            result.Should().BeTrue();
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_Failure_ReturnsFalse(
            [Frozen] Mock<ITruckRepository> mockTruckRepository)
        {
            mockTruckRepository
                .Setup(repo => repo.Delete(0))
                .ReturnsAsync(false);
            var sut = new TruckService(
                mockTruckRepository.Object,
                TruckFixture._mapper);

            var result = await sut.Delete(0);

            result.Should().BeFalse();
        }
    }
}
