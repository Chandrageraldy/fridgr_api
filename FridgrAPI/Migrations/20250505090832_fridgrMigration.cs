using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgrAPI.Migrations
{
    /// <inheritdoc />
    public partial class fridgrMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItem",
                columns: table => new
                {
                    foodItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foodItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    foodItemNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    foodItemQuantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    foodItemUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    foodItemImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    expiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    spaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItem", x => x.foodItemId);
                });

            migrationBuilder.CreateTable(
                name: "Grocery",
                columns: table => new
                {
                    groceryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    groceryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    groceryQuantity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    groceryUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grocery", x => x.groceryId);
                });

            migrationBuilder.CreateTable(
                name: "Space",
                columns: table => new
                {
                    spaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    spaceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    spaceNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    spaceImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Space", x => x.spaceId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItem");

            migrationBuilder.DropTable(
                name: "Grocery");

            migrationBuilder.DropTable(
                name: "Space");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
