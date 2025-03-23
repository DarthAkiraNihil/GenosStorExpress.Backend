using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenosStorExpress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PurgingWarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                schema: "public",
                table: "BankCards",
                type: "character varying(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                schema: "public",
                table: "BankCards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(80)",
                oldMaxLength: 80);
        }
    }
}
