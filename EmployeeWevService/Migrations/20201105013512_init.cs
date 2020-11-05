using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeWevService.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    ProjectName = table.Column<string>(nullable: false),
                    Budget = table.Column<double>(nullable: false),
                    Profit = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    JobTitle = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    picUrl = table.Column<string>(nullable: false),
                    salary = table.Column<double>(nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    ProId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_employees_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "Email", "JobTitle", "Name", "PhoneNumber", "ProId", "ProjectId", "picUrl", "salary" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "h@gmail.com", "accountant", "hana", "01200012010", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), null, "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b36"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "afasfsd", "efsa", "444444", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), null, "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b37"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "a", "l", "5555", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), null, "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b38"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "f", "d", "44444", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), null, "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b39"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "e", "q", "01001001", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), null, "http://nnn.com", 5000.0 }
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "ProjectId", "Budget", "Profit", "ProjectName" },
                values: new object[,]
                {
                    { new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), 12000.0, 1000.0, "Test" },
                    { new Guid("d28888e9-2ba9-473a-d40f-e38cb54f9b38"), 12000.0, 1000.0, "Test" },
                    { new Guid("d29988e9-2ba9-473a-a40f-e38cb54f9b38"), 12000.0, 1000.0, "Test" },
                    { new Guid("d28888e9-2ba9-473a-a40f-e39cb54f9b37"), 12000.0, 1000.0, "Test" },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b31"), 12000.0, 1000.0, "Test" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_ProjectId",
                table: "employees",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
