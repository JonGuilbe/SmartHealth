using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartHealth.Migrations.ApplicationDb
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Services",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MessageTime",
                table: "Messages",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkEnd",
                table: "AspNetUsers",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkStart",
                table: "AspNetUsers",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Appointments",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Appointments",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "MessageTime",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "WorkEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkStart",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan[]>(
                name: "Time",
                table: "Appointments",
                nullable: true);
        }
    }
}
