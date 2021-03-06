using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.API.Controllers;
using GarageVolver.API.Models;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Validators;
using GarageVolver.UnitTest.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Controllers
{
    public class TestCreateTruck
    {
        [Theory]
        [AutoDomainData]
        public async Task CreateTruck_OnSucess_ReturnsStatusCode200Async(
            [Frozen] Mock<ITruckService> mockTruckService,
            CreateTruckModel newTruck,
            GetTruckModel truck)
        {
            mockTruckService
                .Setup(service => service.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Create(newTruck) as ObjectResult;

            result?.StatusCode.Should().Be(200);
        }

        [Theory]
        [AutoDomainData]
        public async Task CreateTruck_OnSucess_InvokesTruckServiceOnce(
            [Frozen] Mock<ITruckService> mockTruckService,
            CreateTruckModel newTruck,
            GetTruckModel truck)
        {
            mockTruckService
                .Setup(service => service.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            await sut.Create(newTruck);

            mockTruckService.Verify(
                service => service.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck), Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task CreateTruck_OnSucess_ReturnTruckModel(
            [Frozen] Mock<ITruckService> mockTruckService,
            CreateTruckModel newTruck,
            GetTruckModel truck)
        {
            mockTruckService
                .Setup(service => service.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Create(newTruck);

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.Value.Should().BeOfType<GetTruckModel>();
        }

        [Theory]
        [AutoDomainData]
        public async Task CreateTruck_OnNullInput_Return400(
            [Frozen] Mock<ITruckService> mockTruckService)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            CreateTruckModel newTruck = null;
            GetTruckModel truck = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
            mockTruckService
                .Setup(service => service.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck))
                .ReturnsAsync(truck);
#pragma warning restore CS8604 // Possible null reference argument.
            var sut = new TruckController(mockTruckService.Object);

#pragma warning disable CS8604 // Possible null reference argument.
            var result = await sut.Create(newTruck);
#pragma warning restore CS8604 // Possible null reference argument.

            result.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = result as BadRequestObjectResult;
            objectResult?.StatusCode.Should().Be(400);
        }

        [Theory]
        [AutoDomainData]
        public async Task CreateTruck_OnInvalidContent_Return409(
            [Frozen] Mock<ITruckService> mockTruckService,
            CreateTruckModel newTruck)
        {
            newTruck.ModelYear = DateTime.Now.Year - 1;
            GetTruckModel? truck = null;
            mockTruckService
                .Setup(service => service.Add<CreateTruckModel, GetTruckModel, TruckValidator>(newTruck))
                .ReturnsAsync(truck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Create(newTruck);

            result.Should().BeOfType<ConflictObjectResult>();
            var objectResult = result as ConflictObjectResult;
            objectResult?.StatusCode.Should().Be(409);
        }
    }
}