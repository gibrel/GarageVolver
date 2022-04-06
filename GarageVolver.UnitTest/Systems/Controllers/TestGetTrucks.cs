using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.API.Controllers;
using GarageVolver.API.Models;
using GarageVolver.Domain.Interfaces;
using GarageVolver.UnitTest.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Controllers
{
    public class TestGetTrucks
    {
        [Theory]
        [AutoDomainData]
        public async Task GetTrucks_OnSucess_ReturnsStatusCode200Async(
            [Frozen] Mock<ITruckService> mockTruckService,
            List<GetTruckModel> trucks)
        {
            mockTruckService
                .Setup(service => service.GetAll<GetTruckModel>())
                .ReturnsAsync(trucks);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get() as ObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTrucks_OnSucess_InvokesTruckServiceOnce(
            [Frozen] Mock<ITruckService> mockTruckService,
            List<GetTruckModel> trucks)
        {
            mockTruckService
                .Setup(service => service.GetAll<GetTruckModel>())
                .ReturnsAsync(trucks);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get();

            mockTruckService.Verify(
                service => service.GetAll<GetTruckModel>(), Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTrucks_OnSucess_ReturnListOfTruckModels(
            [Frozen] Mock<ITruckService> mockTruckService,
            List<GetTruckModel> trucks)
        {
            mockTruckService
                .Setup(service => service.GetAll<GetTruckModel>())
                .ReturnsAsync(trucks);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.Value.Should().BeOfType<List<GetTruckModel>>();
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTrucks_OnNoTrucksFound_Return404(
            [Frozen] Mock<ITruckService> mockTruckService)
        {
            mockTruckService
                .Setup(service => service.GetAll<GetTruckModel>())
                .ReturnsAsync(new List<GetTruckModel>());
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<NotFoundResult>();
            var objectResult = result as NotFoundResult;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}