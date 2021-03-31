using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blasa.travel.persistance.Migrations
{
    public partial class change_table_travel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfDeparture",
                schema: "Tarvel",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "NumberOfPlaces",
                schema: "Tarvel",
                table: "Travels");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDate",
                schema: "Tarvel",
                table: "Travels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberPlaces",
                schema: "Tarvel",
                table: "Travels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureDate",
                schema: "Tarvel",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "NumberPlaces",
                schema: "Tarvel",
                table: "Travels");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDeparture",
                schema: "Tarvel",
                table: "Travels",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPlaces",
                schema: "Tarvel",
                table: "Travels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
