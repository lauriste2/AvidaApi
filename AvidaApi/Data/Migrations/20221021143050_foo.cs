using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvidaApi.Data.Migrations
{
    public partial class foo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_LoanModel_LoanId",
                table: "LoanApplication");

            migrationBuilder.RenameColumn(
                name: "LoanId",
                table: "LoanApplication",
                newName: "LoanID");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplication_LoanId",
                table: "LoanApplication",
                newName: "IX_LoanApplication_LoanID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplication_LoanModel_LoanID",
                table: "LoanApplication",
                column: "LoanID",
                principalTable: "LoanModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_LoanModel_LoanID",
                table: "LoanApplication");

            migrationBuilder.RenameColumn(
                name: "LoanID",
                table: "LoanApplication",
                newName: "LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplication_LoanID",
                table: "LoanApplication",
                newName: "IX_LoanApplication_LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplication_LoanModel_LoanId",
                table: "LoanApplication",
                column: "LoanId",
                principalTable: "LoanModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
