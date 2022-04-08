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
using System.ComponentModel.DataAnnotations;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestGetAllTrucks
    {

        [Theory]
        [AutoDomainData]
        public async Task GetAllTrucks_OnSucess_ReturnsListOfGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            [Range(3,6)] int ammountOfTrucks)
        {
            mockTruckRepository
                .Setup(repo => repo.Select())
                .ReturnsAsync(TruckFixture.GenerateListOfTrucks(ammountOfTrucks));
            var sut = new TruckService(
                mockTruckRepository.Object,
                TruckFixture._mapper);

            var result = await sut.GetAll<GetTruckModel>();

            result.Should().BeOfType<List<GetTruckModel>>();
        }

    }
}
