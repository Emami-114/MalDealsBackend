using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MalDealsBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddParentCategoryIdToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parent_category_id",
                table: "categories",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parent_category_id",
                table: "categories");
        }
    }
}
