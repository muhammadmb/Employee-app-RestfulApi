using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    Profit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    picUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    salary = table.Column<double>(type: "float", nullable: false),
                    departmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Headquarter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Departments_Employees_ManagerEmployeeId",
                        column: x => x.ManagerEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName", "Headquarter", "ManagerEmployeeId", "ManagerId" },
                values: new object[,]
                {
                    { new Guid("12345677-2ba9-473a-a40f-e38cb54f9b35"), "Distribution department", "Alexandria", null, new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") },
                    { new Guid("12345678-2ba9-473a-a40f-e38cb54f9b35"), "Marketing department", "Cairo", null, new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b36") }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Budget", "Profit", "ProjectName" },
                values: new object[,]
                {
                    { new Guid("d28888e9-1111-1111-a401-e38cb54f9b36"), 50000.0, 10000.0, "E-commerce" },
                    { new Guid("d28888e9-1111-1111-a402-e38cb54f9b36"), 8000.0, 1500.0, "Database design" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "Email", "JobTitle", "Name", "PhoneNumber", "departmentId", "picUrl", "salary" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTimeOffset(new DateTime(1964, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "First@gmail.com", "accountant", "sameh", "01200012010", new Guid("12345677-2ba9-473a-a40f-e38cb54f9b35"), "http://f.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b37"), new DateTimeOffset(new DateTime(1989, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "M@gmail.com", "HR", "Mohsen", "01200012010", new Guid("12345677-2ba9-473a-a40f-e38cb54f9b35"), "http://MOHSEN.com", 4000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b36"), new DateTimeOffset(new DateTime(1977, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Ali@gmail.com", "Engineer", "Ali", "01200012010", new Guid("12345678-2ba9-473a-a40f-e38cb54f9b35"), "http://AliEng.com", 8000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b38"), new DateTimeOffset(new DateTime(1978, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "So@gmail.com", "Developer", "Salama", "01200012010", new Guid("12345678-2ba9-473a-a40f-e38cb54f9b35"), "http://salama.com", 2000.0 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProjects",
                columns: new[] { "Id", "EmployeeId", "ProjectId" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("d28888e9-1111-1111-a401-e38cb54f9b36") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("d28888e9-1111-1111-a401-e38cb54f9b36") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b37"), new Guid("d28888e9-1111-1111-a402-e38cb54f9b36") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b36"), new Guid("d28888e9-1111-1111-a402-e38cb54f9b36") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerEmployeeId",
                table: "Departments",
                column: "ManagerEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_EmployeeId",
                table: "EmployeeProjects",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_departmentId",
                table: "Employees",
                column: "departmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_departmentId",
                table: "Employees",
                column: "departmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_ManagerEmployeeId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
