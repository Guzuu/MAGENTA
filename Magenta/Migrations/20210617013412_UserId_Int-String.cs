using Microsoft.EntityFrameworkCore.Migrations;

namespace Magenta.Migrations
{
    public partial class UserId_IntString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ProcessedById",
                table: "Works",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DesignedById",
                table: "Designs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_AspNetUsers_DesignedById",
                table: "Designs",
                column: "DesignedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AddedById",
                table: "Projects",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_ProcessedById",
                table: "Works",
                column: "ProcessedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designs_AspNetUsers_DesignedById",
                table: "Designs");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AddedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_ProcessedById",
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

            migrationBuilder.AlterColumn<int>(
                name: "ProcessedById",
                table: "Works",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessedById1",
                table: "Works",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddedById",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedById1",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignedById",
                table: "Designs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignedById1",
                table: "Designs",
                type: "nvarchar(450)",
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
    }
}
