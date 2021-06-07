using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Magenta.Migrations
{
    public partial class Designs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Designs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDesigned = table.Column<DateTime>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    AttatchmentsPath = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    DesignedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designs_Employees_DesignedById",
                        column: x => x.DesignedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Designs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designs_DesignedById",
                table: "Designs",
                column: "DesignedById");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_ProjectId",
                table: "Designs",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Designs");
        }
    }
}
