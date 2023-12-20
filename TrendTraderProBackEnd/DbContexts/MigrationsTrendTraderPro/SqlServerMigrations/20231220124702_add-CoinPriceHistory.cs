using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContexts.MigrationsTrendTraderPro.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class addCoinPriceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinPriceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    MarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinPriceHistories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinPriceHistories");
        }
    }
}
