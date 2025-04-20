using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MalDealsBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyIdToUserMarketDeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_market_deals",
                table: "user_market_deals");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "user_market_deals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_market_deals",
                table: "user_market_deals",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_market_deals",
                table: "user_market_deals");

            migrationBuilder.DropColumn(
                name: "id",
                table: "user_market_deals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_market_deals",
                table: "user_market_deals",
                column: "user_id");
        }
    }
}
