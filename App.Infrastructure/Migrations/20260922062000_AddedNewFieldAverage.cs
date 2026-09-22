using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFieldAverage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ProcessingTime",
                schema: "dbo",
                table: "FileRecords",
                type: "time",
                nullable: false,
                defaultValueSql: "CAST(GETDATE() AS TIME)",
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AddColumn<decimal>(
                name: "Average",
                schema: "dbo",
                table: "FileRecords",
                type: "DECIMAL(12,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Average",
                schema: "dbo",
                table: "FileRecords");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ProcessingTime",
                schema: "dbo",
                table: "FileRecords",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldDefaultValueSql: "CAST(GETDATE() AS TIME)");
        }
    }
}
