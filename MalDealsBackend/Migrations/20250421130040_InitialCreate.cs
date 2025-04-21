using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MalDealsBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    thumbnail = table.Column<string>(type: "text", nullable: true),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    sub_category_ids = table.Column<string[]>(type: "text[]", nullable: true),
                    parent_category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP WITH TIME ZONE", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "deal_vote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    deal_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deal_vote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "deals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    categories = table.Column<string[]>(type: "text[]", nullable: true),
                    is_free = table.Column<bool>(type: "boolean", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: true),
                    offer_price = table.Column<decimal>(type: "numeric", nullable: true),
                    provider = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    provider_url = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    thumbnail_url = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    images_url = table.Column<string[]>(type: "text[]", nullable: true),
                    user_id = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    tags = table.Column<string[]>(type: "text[]", nullable: true),
                    shipping_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    video_url = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    coupon_code = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    is_publish = table.Column<bool>(type: "boolean", nullable: false),
                    vote_count = table.Column<int>(type: "integer", nullable: false),
                    expiration_date = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP WITH TIME ZONE", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "providers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "swagger_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_swagger_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_data",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    photo_url = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: false),
                    verified = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_data", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "user_market_deals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deal_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_market_deals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    verified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_title",
                table: "categories",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deals_title",
                table: "deals",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_providers_title",
                table: "providers",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tags_title",
                table: "tags",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_name",
                table: "users",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "deal_vote");

            migrationBuilder.DropTable(
                name: "deals");

            migrationBuilder.DropTable(
                name: "providers");

            migrationBuilder.DropTable(
                name: "swagger_users");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "user_data");

            migrationBuilder.DropTable(
                name: "user_market_deals");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
