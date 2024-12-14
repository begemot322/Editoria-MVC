using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Course_Work_Editoria.Migrations
{
    /// <inheritdoc />
    public partial class SeedAuthorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Email", "Name", "Phone", "Surname" },
                values: new object[,]
                {
                    { 1, "alexey.ivanov@example.com", "Алексей", "123-456-7890", "Иванов" },
                    { 2, "maria.petrova@example.com", "Мария", "234-567-8901", "Петрова" },
                    { 3, "ivan.sidorov@example.com", "Иван", "345-678-9012", "Сидоров" },
                    { 4, "elena.kuznetsova@example.com", "Елена", "456-789-0123", "Кузнецова" },
                    { 5, "dmitry.smirnov@example.com", "Дмитрий", "567-890-1234", "Смирнов" },
                    { 6, "olga.fedorova@example.com", "Ольга", "678-901-2345", "Фёдорова" },
                    { 7, "sergey.mikhailov@example.com", "Сергей", "789-012-3456", "Михайлов" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 7);
        }
    }
}
