using AutoFixture.Xunit2;
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
    public class TestDeleteTruck
    {
        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_OnRun_InvokeSaveChangesOnce(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();

            await sut.Insert(truck);
            await sut.Delete(truck.Id);

            mockSQLiteContext.Verify(m => m.SaveChanges(), Times.Exactly(2));
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteTruck_OnRun_InvokeSetOnce(
            [Frozen] Mock<DbSet<Truck>> mockTruckSet,
            [Frozen] Mock<SQLiteContext> mockSQLiteContext)
        {
            mockSQLiteContext
                .Setup(context => context.Trucks)
                .Returns(mockTruckSet.Object);
            var sut = new TruckRepository(mockSQLiteContext.Object);
            var truck = TruckFixture.GenerateTruck();

            await sut.Insert(truck);
            await sut.Delete(truck.Id);

            mockSQLiteContext.Verify(m => m.Set<Truck>(), Times.Exactly(3));
        }
    }
}
