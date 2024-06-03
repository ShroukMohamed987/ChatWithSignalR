using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatWithSignalR.Migrations
{
    public partial class InitialCreatee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Userid1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Userid",
                table: "Messages",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Userid1",
                table: "Messages",
                column: "Userid1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_Userid",
                table: "Messages",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_Userid1",
                table: "Messages",
                column: "Userid1",
                principalTable: "Users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_Userid",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_Userid1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Userid",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Userid1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Userid1",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
