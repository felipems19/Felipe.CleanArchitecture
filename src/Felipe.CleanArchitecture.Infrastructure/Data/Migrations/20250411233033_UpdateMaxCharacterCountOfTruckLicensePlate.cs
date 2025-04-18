﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Felipe.CleanArchitecture.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class UpdateMaxCharacterCountOfTruckLicensePlate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "LicensePlate",
            table: "Trucks",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(10)",
            oldMaxLength: 10);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "LicensePlate",
            table: "Trucks",
            type: "nvarchar(10)",
            maxLength: 10,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50);
    }
}
