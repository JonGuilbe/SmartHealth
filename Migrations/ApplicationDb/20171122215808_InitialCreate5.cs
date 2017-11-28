using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartHealth.Migrations.ApplicationDb
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkStart",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "MessageTime",
                table: "Messages",
                newName: "messagetime");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Appointments",
                newName: "starttime");

            migrationBuilder.AlterColumn<string>(
                name: "messagetime",
                table: "Messages",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "endtime",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "starttime",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "starttime",
                table: "Appointments",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "endtime",
                table: "Appointments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endtime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "starttime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "endtime",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "messagetime",
                table: "Messages",
                newName: "MessageTime");

            migrationBuilder.RenameColumn(
                name: "starttime",
                table: "Appointments",
                newName: "StartTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MessageTime",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkEnd",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkStart",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
