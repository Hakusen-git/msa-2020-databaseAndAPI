﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentSIMS.Migrations
{
    public partial class UpdatedAddressModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "studentID1",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_studentID1",
                table: "Address",
                column: "studentID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Student_studentID1",
                table: "Address",
                column: "studentID1",
                principalTable: "Student",
                principalColumn: "studentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Student_studentID1",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_studentID1",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "studentID1",
                table: "Address");
        }
    }
}
