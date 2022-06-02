using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class seedsdatastatusandevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "EndDate", "Name", "PageName", "StartDate" },
                values: new object[] { 1, new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Городская Академия Юниум", "GAU", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ученик" },
                    { 2, "Преподаватель" },
                    { 3, "Менеджер" },
                    { 4, "Руководитель" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
