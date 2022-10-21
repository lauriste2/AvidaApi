using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvidaApi.Data.Migrations
{
    public partial class foo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Adress_AdressId",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Person",
                newName: "AdressID");

            migrationBuilder.RenameIndex(
                name: "IX_Person_AdressId",
                table: "Person",
                newName: "IX_Person_AdressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Adress_AdressID",
                table: "Person",
                column: "AdressID",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Adress_AdressID",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "AdressID",
                table: "Person",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_AdressID",
                table: "Person",
                newName: "IX_Person_AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Adress_AdressId",
                table: "Person",
                column: "AdressId",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
