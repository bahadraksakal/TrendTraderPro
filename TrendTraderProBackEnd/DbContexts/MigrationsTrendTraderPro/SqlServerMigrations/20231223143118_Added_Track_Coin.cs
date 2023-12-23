using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContexts.MigrationsTrendTraderPro.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class Added_Track_Coin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoinId",
                table: "CoinPriceHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TrackCoin",
                columns: table => new
                {
                    CoinId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackStatus = table.Column<int>(type: "int", nullable: true),
                    LastRequestDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackCoin", x => x.CoinId);
                    table.ForeignKey(
                        name: "FK_TrackCoin_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinPriceHistories_CoinId",
                table: "CoinPriceHistories",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinPriceHistories_Coins_CoinId",
                table: "CoinPriceHistories",
                column: "CoinId",
                principalTable: "Coins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinPriceHistories_Coins_CoinId",
                table: "CoinPriceHistories");

            migrationBuilder.DropTable(
                name: "TrackCoin");

            migrationBuilder.DropIndex(
                name: "IX_CoinPriceHistories_CoinId",
                table: "CoinPriceHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CoinId",
                table: "CoinPriceHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
