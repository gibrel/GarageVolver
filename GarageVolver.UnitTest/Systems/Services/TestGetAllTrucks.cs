using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GarageVolver.API.Models;
using GarageVolver.API.Configurations;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestGetAllTrucks
    {
        private Mapper ConfigureMapper()
        {
            var truckMapProfile = new TruckMapProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(truckMapProfile));
            return new Mapper(configuration);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetAllTrucks_OnSucess_ReturnsListOfGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            List<Truck> getListTruckModel)
        {
            IMapper mapper = ConfigureMapper();
            mockTruckRepository
                .Setup(repo => repo.Select())
                .ReturnsAsync(getListTruckModel);
            var sut = new TruckService(
                mockTruckRepository.Object,
                mapper);

            var result = await sut.GetAll<GetTruckModel>();

            result.Should().BeOfType<List<GetTruckModel>>();
        }

    }
}
