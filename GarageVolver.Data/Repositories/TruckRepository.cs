using GarageVolver.Data.Context;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;

namespace GarageVolver.Data.Repositories
{
    public class TruckRepository : BaseRepository<Truck>, ITruckRepository
    {
        public TruckRepository(SQLiteContext sQLiteContext) : base(sQLiteContext)
        {
        }
    }
}
