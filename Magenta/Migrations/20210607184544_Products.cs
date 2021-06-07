using Microsoft.EntityFrameworkCore.Migrations;

namespace Magenta.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Foil = table.Column<string>(nullable: true),
                    Cover = table.Column<string>(nullable: true),
                    Folding = table.Column<string>(nullable: true),
                    CoverRefinement = table.Column<string>(nullable: true),
                    InsideRefinement = table.Column<string>(nullable: true),
                    Binding = table.Column<string>(nullable: true),
                    Paper = table.Column<string>(nullable: true),
                    InsideColor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
