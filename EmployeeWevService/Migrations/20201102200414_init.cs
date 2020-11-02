using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeWevService.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ProId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    ProjectName = table.Column<string>(nullable: false),
                    Budget = table.Column<double>(nullable: false),
                    Profit = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_projects_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "Email", "JobTitle", "Name", "PhoneNumber", "ProId", "picUrl", "salary" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "h@gmail.com", "accountant", "hana", "01200012010", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b36"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "afasfsd", "efsa", "444444", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b37"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "a", "l", "5555", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b38"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "f", "d", "44444", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), "http://nnn.com", 5000.0 },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b39"), new DateTimeOffset(new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "s@gmail.com", "e", "q", "01001001", new Guid("a28888e9-2ba9-473a-a40f-e38cb54f9b38"), "http://nnn.com", 5000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_projects_EmployeeId",
                table: "projects",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
