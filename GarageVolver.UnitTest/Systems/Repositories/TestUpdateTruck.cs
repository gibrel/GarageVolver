using AutoFixture.Xunit2;
using FluentAssertions;
using GarageVolver.Domain.Entities;
using GarageVolver.UnitTest.Fixtures;
using Moq;
using System.Threading.Tasks;
using Xunit;
using GarageVolver.Data.Repositories;
using GarageVolver.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GarageVolver.UnitTest.Systems.Repositories
{
    public class TestUpdateTruck
    {

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnSucess_ShouldReturnTrue(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();
            await sut.Insert(truck);

            truck = TruckFixture.MakeChanges(truck);
            var result = await sut.Update(truck);

            result.Should().BeTrue();
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnRun_InvokeSaveChangesOnce(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();
            await sut.Insert(truck);

            truck = TruckFixture.MakeChanges(truck);
            var result = await sut.Update(truck);

            mockSQLiteContext.Verify(m => m.SaveChanges(), Times.Exactly(2));
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateTruck_OnRun_InvokeSetOnce(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();
            await sut.Insert(truck);

            truck = TruckFixture.MakeChanges(truck);
            var result = await sut.Update(truck);

            mockSQLiteContext.Verify(m => m.Set<Truck>(), Times.Once());
        }
    }
}
