using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class _310520242349 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamAccount_Teams_TeamsId",
                table: "TeamAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamAccount_User_UsersId",
                table: "TeamAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamAccount",
                table: "TeamAccount");

            migrationBuilder.RenameTable(
                name: "TeamAccount",
                newName: "TeamUser");

            migrationBuilder.RenameIndex(
                name: "IX_TeamAccount_UsersId",
                table: "TeamUser",
                newName: "IX_TeamUser_UsersId");

            migrationBuilder.AlterColumn<string>(
                name: "InteractionType",
                table: "Interactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamUser",
                table: "TeamUser",
                columns: new[] { "TeamsId", "UsersId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_Teams_TeamsId",
                table: "TeamUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_User_UsersId",
                table: "TeamUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamUser",
                table: "TeamUser");

            migrationBuilder.RenameTable(
                name: "TeamUser",
                newName: "TeamAccount");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUser_UsersId",
                table: "TeamAccount",
                newName: "IX_TeamAccount_UsersId");

            migrationBuilder.AlterColumn<int>(
                name: "InteractionType",
                table: "Interactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamAccount",
                table: "TeamAccount",
                columns: new[] { "TeamsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamAccount_Teams_TeamsId",
                table: "TeamAccount",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamAccount_User_UsersId",
                table: "TeamAccount",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
