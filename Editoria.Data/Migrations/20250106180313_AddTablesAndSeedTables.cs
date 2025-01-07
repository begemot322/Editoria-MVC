using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Editoria.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesAndSeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Editors",
                columns: table => new
                {
                    EditorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editors", x => x.EditorId);
                });

            migrationBuilder.CreateTable(
                name: "Newspapers",
                columns: table => new
                {
                    NewspaperId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Circulation = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newspapers", x => x.NewspaperId);
                    table.ForeignKey(
                        name: "FK_Newspapers_Editors_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Editors",
                        principalColumn: "EditorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NewspaperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.IssueId);
                    table.ForeignKey(
                        name: "FK_Issues_Newspapers_NewspaperId",
                        column: x => x.NewspaperId,
                        principalTable: "Newspapers",
                        principalColumn: "NewspaperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    AdvertisementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.AdvertisementId);
                    table.ForeignKey(
                        name: "FK_Advertisements_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Новости о политике", "Политика" },
                    { 2, "Новости экономики и финансов", "Экономика" },
                    { 3, "Новости о культуре и искусстве", "Культура" },
                    { 4, "Спортивные события и новости", "Спорт" },
                    { 5, "Новости технологий и инноваций", "Технологии" },
                    { 6, "Новости о здоровье и медицинских исследованиях", "Здоровье" },
                    { 7, "Новости образования и науки", "Образование" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_IssueId",
                table: "Advertisements",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_IssueId",
                table: "Articles",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_NewspaperId",
                table: "Issues",
                column: "NewspaperId");

            migrationBuilder.CreateIndex(
                name: "IX_Newspapers_EditorId",
                table: "Newspapers",
                column: "EditorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Newspapers");

            migrationBuilder.DropTable(
                name: "Editors");
        }
    }
}
