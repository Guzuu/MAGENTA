using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Magenta.Migrations
{
    public partial class Works : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountProcessed = table.Column<int>(nullable: false),
                    AdditionalInfo = table.Column<string>(nullable: true),
                    DateProcessed = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    WorkTypeId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    ProcessedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Works_Employees_ProcessedById",
                        column: x => x.ProcessedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Works_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Works_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_DepartmentId",
                table: "Works",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_ProcessedById",
                table: "Works",
                column: "ProcessedById");

            migrationBuilder.CreateIndex(
                name: "IX_Works_ProjectId",
                table: "Works",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkTypeId",
                table: "Works",
                column: "WorkTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Works");
        }
    }
}
