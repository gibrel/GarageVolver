using GarageVolver.Data.Configurations;
using GarageVolver.Data.Mapping;
using GarageVolver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GarageVolver.Data.Context
{
    public class SQLiteContext : DbContext
    {
        private readonly SQLiteConfiguration _configuration;

        public SQLiteContext(IOptions<SQLiteConfiguration> configuration) : base()
        {
            _configuration = configuration.Value;
        }
        public virtual DbSet<Truck> Trucks { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filename = _configuration.Filename;
            filename ??= "TestGarageVolver";

            optionsBuilder.UseSqlite($"Filename={filename}.db", opt =>
            {
                opt.CommandTimeout(120);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Truck>(new TruckMap().Configure);
        }
    }
}
