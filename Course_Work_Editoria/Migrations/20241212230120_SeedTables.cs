using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Course_Work_Editoria.Migrations
{
    /// <inheritdoc />
    public partial class SeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Advertisements",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Advertisements",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Newspapers",
                columns: new[] { "NewspaperId", "Circulation", "EditorId", "Name", "Region", "Type" },
                values: new object[,]
                {
                    { 1, 10000, 1, "Городские новости", "Москва", "Ежедневная" },
                    { 2, 5000, 2, "Бизнес-обозрение", "Санкт-Петербург", "Еженедельная" },
                    { 3, 7000, 3, "Технологический вестник", "Новосибирск", "Ежемесячная" },
                    { 4, 3000, 4, "Сельская жизнь", "Краснодар", "Еженедельная" },
                    { 5, 4000, 5, "Культура и искусство", "Казань", "Ежемесячная" },
                    { 6, 6000, 6, "Наука сегодня", "Екатеринбург", "Ежемесячная" },
                    { 7, 8000, 7, "Мир спорта", "Сочи", "Еженедельная" }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "IssueId", "Content", "NewspaperId", "PublicationDate" },
                values: new object[,]
                {
                    { 1, "Новости Москвы", 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Обзор бизнеса", 2, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Новые технологии", 3, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Новости села", 4, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Искусство и культура", 5, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Научные открытия", 6, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Спортивные события", 7, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Advertisements",
                columns: new[] { "AdvertisementId", "Cost", "IssueId", "Text", "Type" },
                values: new object[,]
                {
                    { 1, 100.50m, 1, "This is a banner advertisement.", "Banner" },
                    { 2, 200.00m, 1, "This is a popup advertisement.", "Popup" },
                    { 3, 500.00m, 2, "This is a video advertisement.", "Video" },
                    { 4, 50.00m, 2, "This is a text advertisement.", "Text" },
                    { 5, 120.00m, 3, "This is a sidebar advertisement.", "Sidebar" },
                    { 6, 1000.00m, 3, "This is a full-page advertisement.", "Full-page" },
                    { 7, 150.75m, 4, "Another banner advertisement.", "Banner" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "AuthorId", "CategoryId", "IssueId", "PublicationDate", "Text", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Text of article 1", "Article 1" },
                    { 2, 2, 2, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Text of article 2", "Article 2" },
                    { 3, 3, 3, 2, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Text of article 3", "Article 3" },
                    { 4, 4, 1, 2, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Text of article 4", "Article 4" },
                    { 5, 5, 2, 3, new DateTime(2024, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Text of article 5", "Article 5" },
                    { 6, 6, 3, 3, new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Text of article 6", "Article 6" },
                    { 7, 7, 1, 4, new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Text of article 7", "Article 7" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "AdvertisementId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "AdvertisementId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "AdvertisementId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "AdvertisementId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "AdvertisementId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "AdvertisementId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "AdvertisementId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Newspapers",
                keyColumn: "NewspaperId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Newspapers",
                keyColumn: "NewspaperId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Newspapers",
                keyColumn: "NewspaperId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Newspapers",
                keyColumn: "NewspaperId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Newspapers",
                keyColumn: "NewspaperId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Newspapers",
                keyColumn: "NewspaperId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Newspapers",
                keyColumn: "NewspaperId",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
