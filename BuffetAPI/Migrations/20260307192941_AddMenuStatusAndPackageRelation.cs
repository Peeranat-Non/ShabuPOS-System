using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuffetAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuStatusAndPackageRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuStatus",
                table: "Menu",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MenuPackage",
                columns: table => new
                {
                    MenusMenuId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    PackagesPackage_ID = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPackage", x => new { x.MenusMenuId, x.PackagesPackage_ID });
                    table.ForeignKey(
                        name: "FK_MenuPackage_Menu_MenusMenuId",
                        column: x => x.MenusMenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPackage_Package_PackagesPackage_ID",
                        column: x => x.PackagesPackage_ID,
                        principalTable: "Package",
                        principalColumn: "Package_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuPackage_PackagesPackage_ID",
                table: "MenuPackage",
                column: "PackagesPackage_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuPackage");

            migrationBuilder.DropColumn(
                name: "MenuStatus",
                table: "Menu");
        }
    }
}
