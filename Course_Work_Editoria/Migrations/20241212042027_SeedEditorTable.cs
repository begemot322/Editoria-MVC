using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Course_Work_Editoria.Migrations
{
    /// <inheritdoc />
    public partial class SeedEditorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Authors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Editors",
                columns: new[] { "EditorId", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "anna.ivanova@example.com", "Анна", "Иванова" },
                    { 2, "boris.smirnov@example.com", "Борис", "Смирнов" },
                    { 3, "victor.kuznetsov@example.com", "Виктор", "Кузнецов" },
                    { 4, "galina.petrova@example.com", "Галина", "Петрова" },
                    { 5, "dmitriy.sidorov@example.com", "Дмитрий", "Сидоров" },
                    { 6, "ekaterina.soboleva@example.com", "Екатерина", "Соболева" },
                    { 7, "zhanna.nikolaeva@example.com", "Жанна", "Николаева" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "EditorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "EditorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "EditorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "EditorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "EditorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "EditorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "EditorId",
                keyValue: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
