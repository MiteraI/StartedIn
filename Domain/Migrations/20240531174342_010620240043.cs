using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class _010620240043 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_Teams_TeamsId",
                table: "TeamUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_User_UsersId",
                table: "TeamUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "TeamUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TeamsId",
                table: "TeamUser",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUser_UsersId",
                table: "TeamUser",
                newName: "IX_TeamUser_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleInTeam",
                table: "TeamUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UserId",
                table: "Teams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUser_Teams_TeamId",
                table: "TeamUser",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUser_User_UserId",
                table: "TeamUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_User_UserId",
                table: "Teams",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_Teams_TeamId",
                table: "TeamUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_User_UserId",
                table: "TeamUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_User_UserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_UserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RoleInTeam",
                table: "TeamUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TeamUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TeamUser",
                newName: "TeamsId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUser_UserId",
                table: "TeamUser",
                newName: "IX_TeamUser_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUser_Teams_TeamsId",
                table: "TeamUser",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUser_User_UsersId",
                table: "TeamUser",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
