﻿// <auto-generated />
using GarageVolver.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GarageVolver.Data.Migrations
{
    [DbContext(typeof(SQLiteContext))]
    [Migration("20220408032417_Migration.002")]
    partial class Migration002
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("GarageVolver.Domain.Entities.Truck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LicencePlate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("LicencePlate");

                    b.Property<string>("ManufacturingYear")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ManufacturingYear");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("ModelYear")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ModelYear");

                    b.HasKey("Id");

                    b.ToTable("Truck", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}