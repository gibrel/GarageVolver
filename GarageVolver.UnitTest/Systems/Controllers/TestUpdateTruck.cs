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
    public class TestUpdateTruck
    {
        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnSucess_ReturnsStatusCode200Async(
            [Frozen] Mock<ITruckService> mockTruckService,
            UpdateTruckModel toUpdateTruck,
            GetTruckModel updatedTruck)
        {
            mockTruckService
                .Setup(service =>
                    service.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(toUpdateTruck))
                .ReturnsAsync(updatedTruck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Update(toUpdateTruck) as ObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnSucess_InvokesTruckServiceOnce(
            [Frozen] Mock<ITruckService> mockTruckService,
            UpdateTruckModel toUpdateTruck,
            GetTruckModel updatedTruck)
        {
            mockTruckService
                .Setup(service => service.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(toUpdateTruck))
                .ReturnsAsync(updatedTruck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Update(toUpdateTruck);

            mockTruckService.Verify(
                service => service.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(toUpdateTruck), Times.Once());
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnSucess_ReturnTruckModel(
            [Frozen] Mock<ITruckService> mockTruckService,
            UpdateTruckModel toUpdateTruck,
            GetTruckModel updatedTruck)
        {
            mockTruckService
                .Setup(service => service.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(toUpdateTruck))
                .ReturnsAsync(updatedTruck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Update(toUpdateTruck);

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.Value.Should().BeOfType<GetTruckModel>();
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnNullInput_Return400(
            [Frozen] Mock<ITruckService> mockTruckService)
        {
            UpdateTruckModel? toUpdateTruck = null;
            GetTruckModel? updatedTruck = null;
            mockTruckService
                .Setup(service => service.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(toUpdateTruck))
                .ReturnsAsync(updatedTruck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Update(toUpdateTruck);

            result.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = result as BadRequestObjectResult;
            objectResult.StatusCode.Should().Be(400);
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnInvalidContent_Return409(
            [Frozen] Mock<ITruckService> mockTruckService,
            UpdateTruckModel toUpdateTruck)
        {
            toUpdateTruck.ModelYear = DateTime.Now.Year - 1;
            GetTruckModel? updatedTruck = null;
            mockTruckService
                .Setup(service => service.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(toUpdateTruck))
                .ReturnsAsync(updatedTruck);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Update(toUpdateTruck);

            result.Should().BeOfType<ConflictObjectResult>();
            var objectResult = result as ConflictObjectResult;
            objectResult.StatusCode.Should().Be(409);
        }
    }
}
