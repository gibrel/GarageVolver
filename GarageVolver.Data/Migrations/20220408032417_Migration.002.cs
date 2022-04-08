using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageVolver.Data.Migrations
{
    public partial class Migration002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Truck_LicencePlate",
                table: "Truck");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Truck_LicencePlate",
                table: "Truck",
                column: "LicencePlate");
        }
    }
}
