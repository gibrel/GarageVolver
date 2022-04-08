﻿using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GarageVolver.API.Models;
using GarageVolver.API.Configurations;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using GarageVolver.Service.Validators;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Services
{
    public class TestUpdateTruck
    {
        private Mapper ConfigureMapper()
        {
            var truckMapProfile = new TruckMapProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(truckMapProfile));
            return new Mapper(configuration);
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnSucess_ReturnsGetTruckModel(
            [Frozen] Mock<ITruckRepository> mockTruckRepository,
            [Range(0, 1)] int truckModelId,
            [Range(0, 1)] int truckModelYear)
        {
            IMapper mapper = ConfigureMapper();
            truckModelYear += DateTime.Now.Year;
            Truck truck = new()
            {
                Id = 0,
                Model = Enumeration.GetById<TruckModel>(truckModelId),
                ManufacturingYear = DateTime.Now.Year,
                ModelYear = truckModelYear,
            };
            UpdateTruckModel updateTruck = new()
            {
                ModelName = truck.Model.Name,
                ModelYear = truck.ModelYear,
                ManufacturingYear = truck.ManufacturingYear
            };
            mockTruckRepository
                .Setup(repo => repo.Update(truck))
                .ReturnsAsync(true);
            var sut = new TruckService(
                mockTruckRepository.Object,
                mapper);

            var result = await sut.Update<UpdateTruckModel, GetTruckModel, TruckValidator>(updateTruck);

            result.Should().BeOfType<GetTruckModel>();
        }

    }
}