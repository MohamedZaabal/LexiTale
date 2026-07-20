using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiTale.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newlychanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_UserId",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_UsrId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_UsrId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "UsrId",
                table: "Words");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Words",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Words",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_UserId1",
                table: "Words",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_UserId",
                table: "Words",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_UserId1",
                table: "Words",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_UserId",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_UserId1",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_UserId1",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Words");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Words",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UsrId",
                table: "Words",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Words_UsrId",
                table: "Words",
                column: "UsrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_UserId",
                table: "Words",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_UsrId",
                table: "Words",
                column: "UsrId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
