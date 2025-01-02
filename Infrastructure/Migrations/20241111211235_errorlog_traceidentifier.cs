using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class errorlog_traceidentifier : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "TraceIdentifier",
            table: "ErrorLogs",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Users_Email",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "TraceIdentifier",
            table: "ErrorLogs");
    }
}