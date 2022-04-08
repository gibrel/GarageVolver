using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageVolver.Data.Mapping
{
    public class TruckMap : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.ToTable("Truck");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Model)
                .IsRequired()
                .HasConversion(
                    p => p.ToString(),
                    p => Enumeration.GetByName<TruckModel>(p))
                .HasColumnName("CreatedDate")
                .HasColumnType("INTEGER");

            builder.Property(t => t.ModelYear)
                .IsRequired()
                .HasConversion(p => p.ToString(), p => int.Parse(p))
                .HasColumnName("ModelYear")
                .HasColumnType("INTEGER");

            builder.Property(t => t.ManufacturingYear)
                .IsRequired()
                .HasConversion(p => p.ToString(), p => int.Parse(p))
                .HasColumnName("ManufacturingYear")
                .HasColumnType("INTEGER");

            builder.Property(t => t.LicencePlate)
                .IsRequired()
                .HasColumnName("LicencePlate")
                .HasColumnType("TEXT");
        }
    }
}
