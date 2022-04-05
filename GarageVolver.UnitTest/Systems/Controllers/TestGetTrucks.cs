using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.API.Controllers;
using GarageVolver.API.Models;
using GarageVolver.Domain.Interfaces;
using GarageVolver.UnitTest.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            List<GetTruckModel> Trucks)
        {
            mockTruckService
                .Setup(service => service.GetAll<GetTruckModel>())
                .ReturnsAsync(Trucks);
            var sut = new TruckController(mockTruckService.Object);

            var result = await sut.Get() as ObjectResult;

            result.StatusCode.Should().Be(200);
        }
    }
}
