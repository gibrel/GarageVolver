using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.Data.Context;
using GarageVolver.Data.Repositories;
using GarageVolver.Domain.Entities;
using GarageVolver.UnitTest.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Repositories
{
    public class TestSelecTrucks
    {
        [Theory]
        [AutoDomainData]
        public async Task SelectTrucks_OnRun_InvokeSaveChanges(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext,
            [Range(3, 6)] int numberOfTrucks)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);

            List<Truck> insertedTrucks = new();
            for (int i = 0; i < numberOfTrucks; i++)
            {
                var truck = TruckFixture.GenerateTruck();
                await sut.Insert(truck);
                insertedTrucks.Add(truck);
            }
            await sut.Select();

            mockSQLiteContext.Verify(m => m.Set<Truck>(), Times.AtMost(numberOfTrucks + 1));
        }

        [Theory]
        [AutoDomainData]
        public async Task SelectTrucks_OnSucess_ShouldReturnListOfTrucks(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext,
            [Range(3, 6)] int numberOfTrucks)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);

            List<Truck> insertedTrucks = new();
            for (int i = 0; i < numberOfTrucks; i++)
            {
                var truck = TruckFixture.GenerateTruck();
                await sut.Insert(truck);
                insertedTrucks.Add(truck);
            }
            var result = await sut.Select();

            result.Should().BeOfType<List<Truck>>();
        }

        [Theory]
        [AutoDomainData]
        public async Task SelectTrucks_OnSucess_ShouldHaveExpectedSize(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext,
            [Range(3, 6)] int numberOfTrucks)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);

            List<Truck> insertedTrucks = new();
            for (int i = 0; i < numberOfTrucks; i++)
            {
                var truck = TruckFixture.GenerateTruck();
                await sut.Insert(truck);
                insertedTrucks.Add(truck);
            }
            var result = await sut.Select();

            result.Count.Should().BeGreaterThanOrEqualTo(insertedTrucks.Count);
        }
    }
}
