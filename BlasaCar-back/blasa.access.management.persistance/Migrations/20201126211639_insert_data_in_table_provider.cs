using Microsoft.EntityFrameworkCore.Migrations;

namespace blasa.access.management.persistance.Migrations
{
    public partial class insert_data_in_table_provider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { 1, "Facebook" },
                    { 2, "Gmail" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
