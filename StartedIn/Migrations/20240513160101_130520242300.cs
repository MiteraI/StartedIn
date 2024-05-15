using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartedIn.Migrations
{
    /// <inheritdoc />
    public partial class _130520242300 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Users",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "profilePicture",
                table: "Users",
                newName: "ProfilePicture");

            migrationBuilder.RenameColumn(
                name: "lastUpdatedTime",
                table: "Users",
                newName: "LastUpdatedTime");

            migrationBuilder.RenameColumn(
                name: "createdTime",
                table: "Users",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "coverPhoto",
                table: "Users",
                newName: "CoverPhoto");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Users",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "bio",
                table: "Users",
                newName: "Bio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Users",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Users",
                newName: "profilePicture");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedTime",
                table: "Users",
                newName: "lastUpdatedTime");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Users",
                newName: "createdTime");

            migrationBuilder.RenameColumn(
                name: "CoverPhoto",
                table: "Users",
                newName: "coverPhoto");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Users",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Users",
                newName: "bio");
        }
    }
}
