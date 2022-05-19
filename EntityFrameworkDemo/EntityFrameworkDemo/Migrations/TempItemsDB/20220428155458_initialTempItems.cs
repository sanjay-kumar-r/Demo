using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkDemo.Migrations.TempItemsDB
{
    public partial class initialTempItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Departments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Departments", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Employees",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DOJ = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DepartmentId = table.Column<int>(type: "int", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Employees", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Employees_Departments_DepartmentId",
            //            column: x => x.DepartmentId,
            //            principalTable: "Departments",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "TempItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_TempId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempItems_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employees_DepartmentId",
            //    table: "Employees",
            //    column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TempItems_EmployeeId",
                table: "TempItems",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempItems");

            //migrationBuilder.DropTable(
            //    name: "Employees");

            //migrationBuilder.DropTable(
            //    name: "Departments");
        }
    }
}
