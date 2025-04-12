using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Felipe.CleanArchitecture.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddInitialModel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Trucks",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LicensePlate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Trucks", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Trucks");
    }
}
