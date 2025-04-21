using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenosStorExpress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                schema: "public",
                table: "Review",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_CustomerId",
                schema: "public",
                table: "Review",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Customer_CustomerId",
                schema: "public",
                table: "Review",
                column: "CustomerId",
                principalSchema: "public",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Customer_CustomerId",
                schema: "public",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_CustomerId",
                schema: "public",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "public",
                table: "Review");
        }
    }
}
