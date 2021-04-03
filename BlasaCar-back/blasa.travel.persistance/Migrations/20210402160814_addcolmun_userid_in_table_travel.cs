using Microsoft.EntityFrameworkCore.Migrations;

namespace blasa.travel.persistance.Migrations
{
    public partial class addcolmun_userid_in_table_travel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<string>(
                name: "Userid",
                schema: "Tarvel",
                table: "Travels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "idUser",
                schema: "Tarvel",
                table: "Travels",
                type: "text",
                nullable: true);
        }
    }
}
