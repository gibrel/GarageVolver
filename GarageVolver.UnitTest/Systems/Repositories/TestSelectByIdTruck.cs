using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.Data.Context;
using GarageVolver.Data.Repositories;
using GarageVolver.Domain.Entities;
using GarageVolver.UnitTest.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Repositories
{
    public class TestSelectByIdTruck
    {

        [Theory]
        [AutoDomainData]
        public async Task SelectByIdTruck_OnRun_InvokeSaveChangesOnce(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();

            await sut.Insert(truck);
            await sut.Select(truck.Id);

            mockSQLiteContext.Verify(m => m.Set<Truck>(), Times.Exactly(2));
        }

        [Theory]
        [AutoDomainData]
        public async Task SelectByIdTruck_OnSucess_ShouldReturnTruck(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();

            await sut.Insert(truck);
            var result = await sut.Select(truck.Id);

            result.Should().BeOfType<Truck>();
        }

        [Theory]
        [AutoDomainData]
        public async Task SelectByIdTruck_OnSucess_ShouldHaveSameId(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();

            await sut.Insert(truck);
            var result = await sut.Select(truck.Id);

            result?.Id.Should().Be(truck.Id);
        }
    }
}
