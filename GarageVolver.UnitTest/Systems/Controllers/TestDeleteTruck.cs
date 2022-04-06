using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.API.Controllers;
using GarageVolver.Domain.Interfaces;
using GarageVolver.UnitTest.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Controllers
{
    public class TestDeleteTruck
    {
        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_OnSucess_ReturnsStatusCode200Async(
            [Frozen] Mock<ITruckService> mockTruckService,
            int truckId)
        {
            mockTruckService
                .Setup(service => service.Delete(truckId))
                .ReturnsAsync(true);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Delete(truckId) as ObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_OnSucess_InvokesTruckServiceOnce(
            [Frozen] Mock<ITruckService> mockTruckService,
            int truckId)
        {
            mockTruckService
                .Setup(service => service.Delete(truckId))
                .ReturnsAsync(true);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Delete(truckId);

            mockTruckService.Verify(
                service => service.Delete(truckId), Times.Once());
        }

        [Theory]
        [InlineAutoData(0)]
        [InlineAutoData(-1)]
        public async Task DeleteTruck_OnInvalidInput_Return400(
            int truckId,
            [Frozen] Mock<ITruckService> mockTruckService)
        {
            mockTruckService
                .Setup(service => service.Delete(truckId))
                .ReturnsAsync(false);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Delete(truckId);

            result.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = result as BadRequestObjectResult;
            objectResult.StatusCode.Should().Be(400);
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_OnNoTruckFound_Return404(
            [Frozen] Mock<ITruckService> mockTruckService,
            int truckId)
        {
            mockTruckService
                .Setup(service => service.Delete(truckId))
                .ReturnsAsync(false);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Delete(truckId);

            result.Should().BeOfType<NotFoundObjectResult>();
            var objectResult = result as NotFoundObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}
