using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Editoria.Data.Migrations
{
    /// <inheritdoc />
    public partial class addColumnToTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 1,
                column: "Popularity",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 2,
                column: "Popularity",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 3,
                column: "Popularity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 4,
                column: "Popularity",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 5,
                column: "Popularity",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Tags");
        }
    }
}
