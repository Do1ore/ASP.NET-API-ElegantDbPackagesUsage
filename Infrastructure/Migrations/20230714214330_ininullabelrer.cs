﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ininullabelrer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Photographers_PhotographerId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PhotographerId",
                table: "Photos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotographerId",
                table: "Photos",
                column: "PhotographerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Photographers_PhotographerId",
                table: "Photos",
                column: "PhotographerId",
                principalTable: "Photographers",
                principalColumn: "Id");
        }
    }
}
