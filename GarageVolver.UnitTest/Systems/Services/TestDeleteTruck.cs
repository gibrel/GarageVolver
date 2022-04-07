using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GarageVolver.API.Configurations;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestDeleteTruck
    {
        private Mapper ConfigureMapper()
        {
            var truckMapProfile = new TruckMapProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(truckMapProfile));
            return new Mapper(configuration);
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_OnSucess_ReturnsTrue(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            Truck toBeDeletedTruck)
        {
            IMapper mapper = ConfigureMapper();
            mockTruckRepository
                .Setup(repo => repo.Delete(toBeDeletedTruck.Id))
                .ReturnsAsync(true);
            var sut = new TruckService(
                mockTruckRepository.Object,
                mapper);

            var result = await sut.Delete(toBeDeletedTruck.Id);

            result.Should().BeTrue();
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_Failure_ReturnsFalse(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            Truck toBeDeletedTruck)
        {
            toBeDeletedTruck.Id = 0;
            IMapper mapper = ConfigureMapper();
            mockTruckRepository
                .Setup(repo => repo.Delete(toBeDeletedTruck.Id))
                .ReturnsAsync(false);
            var sut = new TruckService(
                mockTruckRepository.Object,
                mapper);

            var result = await sut.Delete(toBeDeletedTruck.Id);

            result.Should().BeFalse();
        }
    }
}
