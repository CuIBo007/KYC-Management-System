using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kyc.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMunicipalityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Municipality",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Municipality");
        }
    }
}
