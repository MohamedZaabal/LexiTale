using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiTale.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FinalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Users_UserId1",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_UserId1",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_UserId1",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_UserId1",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Exercises");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Words",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_UserId1",
                table: "Words",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_UserId1",
                table: "Exercises",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Users_UserId1",
                table: "Exercises",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_UserId1",
                table: "Words",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
