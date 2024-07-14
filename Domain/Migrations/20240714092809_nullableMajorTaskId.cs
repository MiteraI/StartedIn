using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class nullableMajorTaskId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorTasks_MajorTasks_MajorTaskId",
                table: "MinorTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_MinorTasks_Taskboards_TaskboardId",
                table: "MinorTasks");

            migrationBuilder.AlterColumn<string>(
                name: "TaskboardId",
                table: "MinorTasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MajorTaskId",
                table: "MinorTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorTasks_MajorTasks_MajorTaskId",
                table: "MinorTasks",
                column: "MajorTaskId",
                principalTable: "MajorTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorTasks_Taskboards_TaskboardId",
                table: "MinorTasks",
                column: "TaskboardId",
                principalTable: "Taskboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorTasks_MajorTasks_MajorTaskId",
                table: "MinorTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_MinorTasks_Taskboards_TaskboardId",
                table: "MinorTasks");

            migrationBuilder.AlterColumn<string>(
                name: "TaskboardId",
                table: "MinorTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MajorTaskId",
                table: "MinorTasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MinorTasks_MajorTasks_MajorTaskId",
                table: "MinorTasks",
                column: "MajorTaskId",
                principalTable: "MajorTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinorTasks_Taskboards_TaskboardId",
                table: "MinorTasks",
                column: "TaskboardId",
                principalTable: "Taskboards",
                principalColumn: "Id");
        }
    }
}
