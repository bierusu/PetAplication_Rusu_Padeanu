using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAplication.Migrations
{
    public partial class petreserv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetID",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_PetID",
                table: "Request",
                column: "PetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Pet_PetID",
                table: "Request",
                column: "PetID",
                principalTable: "Pet",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Pet_PetID",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_PetID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PetID",
                table: "Request");
        }
    }
}
