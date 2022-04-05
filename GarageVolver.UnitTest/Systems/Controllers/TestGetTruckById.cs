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
    public class TestGetTruckById
    {
        [Theory]
        [AutoDomainData]
        public async Task GetTruckById_OnSucess_ReturnsStatusCode200Async(
            [Frozen] Mock<ITruckService> mockTruckService,
            GetTruckModel truck)
        {
            mockTruckService
                .Setup(service => service.GetById<GetTruckModel>(truck.Id))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get(truck.Id) as ObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTruckById_OnSucess_InvokesTruckServiceOnce(
            [Frozen] Mock<ITruckService> mockTruckService,
            GetTruckModel truck)
        {
            mockTruckService
                .Setup(service => service.GetById<GetTruckModel>(truck.Id))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get(truck.Id);

            mockTruckService.Verify(
                service => service.GetById<GetTruckModel>(truck.Id), Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTruckById_OnSucess_ReturnTruck(
            [Frozen] Mock<ITruckService> mockTruckService,
            GetTruckModel truck)
        {
            mockTruckService
                .Setup(service => service.GetById<GetTruckModel>(truck.Id))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get(truck.Id);

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.Value.Should().BeOfType<GetTruckModel>();
        }

        [Theory]
        [AutoDomainData]
        public async Task GetTruckById_OnNoTruckFound_Return404(
            [Frozen] Mock<ITruckService> mockTruckService,
            int truckId)
        {
            GetTruckModel? truck = null;
            mockTruckService
                .Setup(service => service.GetById<GetTruckModel>(truckId))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get(truckId);

            result.Should().BeOfType<NotFoundObjectResult>();
            var objectResult = result as NotFoundObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}