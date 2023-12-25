using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContexts.MigrationsTrendTraderPro.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class Fix_Added_Track_Coin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackCoin_Coins_CoinId",
                table: "TrackCoin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackCoin",
                table: "TrackCoin");

            migrationBuilder.RenameTable(
                name: "TrackCoin",
                newName: "TrackCoins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackCoins",
                table: "TrackCoins",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackCoins_Coins_CoinId",
                table: "TrackCoins",
                column: "CoinId",
                principalTable: "Coins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackCoins_Coins_CoinId",
                table: "TrackCoins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackCoins",
                table: "TrackCoins");

            migrationBuilder.RenameTable(
                name: "TrackCoins",
                newName: "TrackCoin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackCoin",
                table: "TrackCoin",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackCoin_Coins_CoinId",
                table: "TrackCoin",
                column: "CoinId",
                principalTable: "Coins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
