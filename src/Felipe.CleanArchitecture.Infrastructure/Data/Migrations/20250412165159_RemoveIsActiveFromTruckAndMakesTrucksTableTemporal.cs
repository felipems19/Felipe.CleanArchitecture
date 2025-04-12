using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Felipe.CleanArchitecture.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class RemoveIsActiveFromTruckAndMakesTrucksTableTemporal : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "Trucks");

        migrationBuilder.AlterTable(
            name: "Trucks")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<DateTime>(
            name: "RegisteredAt",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedDate",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<string>(
            name: "Model",
            table: "Trucks",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50)
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<string>(
            name: "LicensePlate",
            table: "Trucks",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50)
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<Guid>(
            name: "Id",
            table: "Trucks",
            type: "uniqueidentifier",
            nullable: false,
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
            name: "PeriodEnd",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
            name: "PeriodStart",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
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
            name: "PeriodEnd",
            table: "Trucks")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
            name: "PeriodStart",
            table: "Trucks")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
            name: "Trucks")
            .OldAnnotation("SqlServer:IsTemporal", true)
            .OldAnnotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
            .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<DateTime>(
            name: "RegisteredAt",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2")
            .OldAnnotation("SqlServer:IsTemporal", true)
            .OldAnnotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
            .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedDate",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2")
            .OldAnnotation("SqlServer:IsTemporal", true)
            .OldAnnotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
            .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<string>(
            name: "Model",
            table: "Trucks",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50)
            .OldAnnotation("SqlServer:IsTemporal", true)
            .OldAnnotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
            .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<string>(
            name: "LicensePlate",
            table: "Trucks",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50)
            .OldAnnotation("SqlServer:IsTemporal", true)
            .OldAnnotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
            .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Trucks",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2")
            .OldAnnotation("SqlServer:IsTemporal", true)
            .OldAnnotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
            .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterColumn<Guid>(
            name: "Id",
            table: "Trucks",
            type: "uniqueidentifier",
            nullable: false,
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier")
            .OldAnnotation("SqlServer:IsTemporal", true)
            .OldAnnotation("SqlServer:TemporalHistoryTableName", "TrucksHistory")
            .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
            .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
            .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "Trucks",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }
}
