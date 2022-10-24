using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvidaApi.Data.Migrations
{
    public partial class foo4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "LoanApplication",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "LoanApplication");
        }
    }
}
