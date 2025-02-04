using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorAPP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SensorData",
                table: "SensorData");

            migrationBuilder.RenameTable(
                name: "SensorData",
                newName: "ExistingSensorData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExistingSensorData",
                table: "ExistingSensorData",
                column: "Guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExistingSensorData",
                table: "ExistingSensorData");

            migrationBuilder.RenameTable(
                name: "ExistingSensorData",
                newName: "SensorData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SensorData",
                table: "SensorData",
                column: "Guid");
        }
    }
}
