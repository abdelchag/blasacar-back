using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace blasa.access.management.persistance.Migrations
{
    public partial class add_table_provider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProviderId",
                table: "AspNetUsers",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Provider_ProviderId",
                table: "AspNetUsers",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Provider_ProviderId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProviderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "AspNetUsers");
        }
    }
}
