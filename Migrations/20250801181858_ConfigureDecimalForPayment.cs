using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScribeTracker.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureDecimalForPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SelfPub",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelfPub",
                table: "Submissions");
        }
    }
}
