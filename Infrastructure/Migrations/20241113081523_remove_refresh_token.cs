using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class remove_refresh_token : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RefreshToken",
            table: "Tokens");

        migrationBuilder.DropColumn(
            name: "RefreshTokenExpireDate",
            table: "Tokens");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "RefreshToken",
            table: "Tokens",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<DateTime>(
            name: "RefreshTokenExpireDate",
            table: "Tokens",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }
}