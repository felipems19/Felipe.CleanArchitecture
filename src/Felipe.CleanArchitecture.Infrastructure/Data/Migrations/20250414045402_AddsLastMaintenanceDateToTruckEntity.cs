using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Felipe.CleanArchitecture.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddsLastMaintenanceDateToTruckEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "LastMaintenanceDate",
            table: "Trucks",
            type: "datetime2",
            nullable: true)
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "LastMaintenanceDate",
            table: "Trucks")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
    }
}
