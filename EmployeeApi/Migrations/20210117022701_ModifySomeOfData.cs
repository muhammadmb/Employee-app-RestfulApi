using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeApi.Migrations
{
    public partial class ModifySomeOfData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "sameh", "soltan" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b36"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Ali", "Hassan" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b37"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Mohsen", "salman" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b38"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Salama", "ahmed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                column: "Name",
                value: "sameh");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b36"),
                column: "Name",
                value: "Ali");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b37"),
                column: "Name",
                value: "Mohsen");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b38"),
                column: "Name",
                value: "Salama");
        }
    }
}
