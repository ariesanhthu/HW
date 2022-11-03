using Microsoft.EntityFrameworkCore.Migrations;

namespace HW.Migrations
{
    public partial class addUserIdfieldtoStuClastb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StudentRooms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentRooms");
        }
    }
}
