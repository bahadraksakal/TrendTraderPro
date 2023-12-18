using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContexts.MigrationsTrendTraderPro.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class update_Coin_Symbol_Name_dontUnique_isrequire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coins_Name",
                table: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_Coins_Symbol",
                table: "Coins");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Coins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Coins",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coins",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_Name",
                table: "Coins",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_Symbol",
                table: "Coins",
                column: "Symbol",
                unique: true,
                filter: "[Symbol] IS NOT NULL");
        }
    }
}
