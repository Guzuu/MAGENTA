using Microsoft.EntityFrameworkCore.Migrations;

namespace Magenta.Migrations
{
    public partial class EmployeesToIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designs_Employees_DesignedById",
                table: "Designs");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_AddedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Departments_DepartmentId",
                table: "Works");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Employees_ProcessedById",
                table: "Works");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Works_DepartmentId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_ProcessedById",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AddedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Designs_DesignedById",
                table: "Designs");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Works");

            migrationBuilder.AddColumn<string>(
                name: "ProcessedById1",
                table: "Works",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedById1",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignedById1",
                table: "Designs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_ProcessedById1",
                table: "Works",
                column: "ProcessedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AddedById1",
                table: "Projects",
                column: "AddedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_DesignedById1",
                table: "Designs",
                column: "DesignedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_AspNetUsers_DesignedById1",
                table: "Designs",
                column: "DesignedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AddedById1",
                table: "Projects",
                column: "AddedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_ProcessedById1",
                table: "Works",
                column: "ProcessedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designs_AspNetUsers_DesignedById1",
                table: "Designs");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AddedById1",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_ProcessedById1",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_ProcessedById1",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AddedById1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Designs_DesignedById1",
                table: "Designs");

            migrationBuilder.DropColumn(
                name: "ProcessedById1",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "AddedById1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DesignedById1",
                table: "Designs");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanBeRemote = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remote = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
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
                name: "IX_Projects_AddedById",
                table: "Projects",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Designs_DesignedById",
                table: "Designs",
                column: "DesignedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_Employees_DesignedById",
                table: "Designs",
                column: "DesignedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_AddedById",
                table: "Projects",
                column: "AddedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Departments_DepartmentId",
                table: "Works",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Employees_ProcessedById",
                table: "Works",
                column: "ProcessedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
