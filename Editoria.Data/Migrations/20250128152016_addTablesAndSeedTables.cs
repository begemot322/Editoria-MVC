using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Editoria.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTablesAndSeedTables : Migration
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
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editors", x => x.EditorId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popularity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                    Information = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    AuthorComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => new { x.ArticleId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ArticleTags_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Biography", "DateOfBirth", "Email", "Name", "Phone", "Surname", "WorkExperience" },
                values: new object[,]
                {
                    { 1, "Алексей Иванов — опытный журналист с более чем 10-летним стажем работы в различных СМИ. Он освещал важные политические события, а также занимается аналитическими расследованиями. Алексей — автор нескольких книг и статей, которые стали знаковыми в своей области.", new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alexey.ivanov@example.com", "Алексей", "123-456-7890", "Иванов", "Работа в газете 'Вести' (2008-2015), журнал 'Мир новостей' (2016-2020), автор статей на различные темы." },
                    { 2, "Мария Петрова — редактор с сильными навыками работы с контентом и журналистскими расследованиями. Она активно сотрудничает с ведущими СМИ и ведет образовательные курсы по редактированию. Мария также является экспертом в области цифровых медиа и новых форматов журналистики.", new DateTime(1990, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria.petrova@example.com", "Мария", "234-567-8901", "Петрова", "Опыт работы в интернет-издании 'ТехноНовости' (2015-2018), автор статей в журнале 'Бизнес и технологии' (2019-2021)." },
                    { 3, "Иван Сидоров — опытный редактор с фокусом на научные и технические публикации. Он работает с высококвалифицированными экспертами в области технологий и науки, обеспечивая точность и ясность материалов. Иван также активно участвует в редакторских советах научных журналов и конференций.", new DateTime(1985, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivan.sidorov@example.com", "Иван", "345-678-9012", "Сидоров", "Работа в журнале 'Наука и технологии' (2010-2017), редактор в научном отделе газеты 'Техник'." },
                    { 4, "Елена Кузнецова — журналист и редактор, специализирующаяся на культурных событиях и социальных вопросах. Она активно освещает темы, связанные с современным искусством, общественными движениями и правами человека. Елена также организует общественные проекты, направленные на развитие культурных инициатив.", new DateTime(1992, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "elena.kuznetsova@example.com", "Елена", "456-789-0123", "Кузнецова", "Опыт работы в журнале 'Культура и искусство' (2014-2018), автор статей в новостном портале 'Свет культуры'." },
                    { 5, "Дмитрий Смирнов — редактор с опытом работы в новостных и политических изданиях. Он специализируется на анализе политических событий и подготовке репортажей с горячих точек. Дмитрий также активно работает с экспертами и политическими аналитиками для создания глубоких и объективных материалов.", new DateTime(1988, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "dmitry.smirnov@example.com", "Дмитрий", "567-890-1234", "Смирнов", "Работа в газете 'Политика сегодня' (2012-2017), редактор в новостном агентстве 'Мир сегодня'." },
                    { 6, "Ольга Фёдорова — начинающий редактор с большим потенциалом и интересом к культурным и образовательным темам. Она активно участвует в создании контента, освещающего важные события в сфере образования и культуры. Ольга также стремится развивать свои навыки в области журналистских расследований и аналитики.", new DateTime(1995, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "olga.fedorova@example.com", "Ольга", "678-901-2345", "Фёдорова", "Работа в стартап-издании 'Культурная эволюция' (2017-2020)." },
                    { 7, "Сергей Михайлов — эксперт по экономическим и политическим темам, работающий в крупных новостных агентствах. Он анализирует ключевые экономические тренды и политические процессы, предлагая экспертные оценки и прогнозы. Сергей также ведет лекции и семинары для профессионалов в области журналистики и аналитики.", new DateTime(1983, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "sergey.mikhailov@example.com", "Сергей", "789-012-3456", "Михайлов", "Работа в агентстве 'Новости России' (2009-2014), автор статей в международном новостном агентстве 'World News'." }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "IsActive", "Name", "Priority" },
                values: new object[,]
                {
                    { 1, "Новости о политике", true, "Политика", 5 },
                    { 2, "Новости экономики и финансов", true, "Экономика", 5 },
                    { 3, "Новости о культуре и искусстве", true, "Культура", 3 },
                    { 4, "Спортивные события и новости", true, "Спорт", 4 },
                    { 5, "Новости технологий и инноваций", true, "Технологии", 4 },
                    { 6, "Новости о здоровье и медицинских исследованиях", true, "Здоровье", 3 },
                    { 7, "Новости образования и науки", true, "Образование", 2 },
                    { 8, "Новости о киберспорте", false, "Киберспорт", 1 }
                });

            migrationBuilder.InsertData(
                table: "Editors",
                columns: new[] { "EditorId", "Biography", "DateOfBirth", "Email", "Name", "Phone", "Surname", "WorkExperience" },
                values: new object[,]
                {
                    { 1, "Анна Иванова — опытный редактор с многолетним стажем работы в различных изданиях. Она специализируется на редактуре материалов по вопросам политики и международных отношений. Анна участвовала в создании множества успешных проектов и редакционных материалов, известных на региональном уровне.", new DateTime(1985, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.ivanova@example.com", "Анна", "123-456-7890", "Иванова", "Работа в редакции газеты 'Молодёжный вестник' (2008-2015), в журнале 'Женская точка зрения' (2016-2021)." },
                    { 2, "Борис Смирнов — опытный редактор в области политики и экономики. Он работал в крупнейших политических изданиях, освещая важнейшие события в экономике и политике. Борис также автор ряда аналитических статей и материалов, которые стали основой для дискуссий в экспертных кругах.", new DateTime(1980, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "boris.smirnov@example.com", "Борис", "234-567-8901", "Смирнов", "Опыт работы в газете 'Политика сегодня' (2006-2015), редактор в журнале 'Экономика и бизнес' (2016-2020)." },
                    { 3, "Виктор Кузнецов — редактор с опытом работы в области культуры и искусства. Он является экспертам в области театра и изобразительного искусства, и его статьи об этих темах пользовались большой популярностью. Виктор также активно участвует в культурных проектах и организует мероприятия для художников и критиков.", new DateTime(1990, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "victor.kuznetsov@example.com", "Виктор", "345-678-9012", "Кузнецов", "Работа в журнале 'Культура и искусство' (2012-2018), редактор в культурном отделе газеты 'Новости культуры'." },
                    { 4, "Галина Петрова — редактор с опытом работы в сфере здравоохранения и экологии. Она специализируется на освещении экологических проблем и вопросов здравоохранения в современных условиях. Галина активно занимается развитием экологических программ и общественных инициатив в сфере охраны природы.", new DateTime(1987, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "galina.petrova@example.com", "Галина", "456-789-0123", "Петрова", "Работа в журнале 'Зеленая планета' (2010-2016), редактор в отделе экологии газеты 'Здоровье и жизнь'." },
                    { 5, "Дмитрий Сидоров — журналист и редактор с многолетним стажем работы в области науки. Он активно участвует в популяризации науки и технологий, освещая самые актуальные достижения в этих областях. Дмитрий является автором многочисленных научных публикаций и работ в области информатики и медицины.", new DateTime(1984, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "dmitriy.sidorov@example.com", "Дмитрий", "567-890-1234", "Сидоров", "Опыт работы в журнале 'Наука и технологии' (2009-2017), редактор в новостном агентстве 'Мир науки'." },
                    { 6, "Екатерина Соболева — молодой редактор с интересом к образовательной тематике. Она увлечена развитием цифрового образования и освещением новых подходов в обучении. Екатерина активно работает с образовательными проектами и занимается разработкой курсов для онлайн-образования.", new DateTime(1993, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "ekaterina.soboleva@example.com", "Екатерина", "678-901-2345", "Соболева", "Работа в онлайн-издании 'Образование будущего' (2015-2019). Начинающий редактор в проекте 'Образование и технологии'." },
                    { 7, "Жанна Николаева — редактор и журналист, специализирующаяся на новостях о жизни города. Она известна своими репортажами о социальных проблемах и городских инициативах. Жанна активно работает с городской администрацией и местными НКО для решения актуальных вопросов жизни в городе.", new DateTime(1991, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "zhanna.nikolaeva@example.com", "Жанна", "789-012-3456", "Николаева", "Работа в газете 'Городские новости' (2014-2019), редактор новостного отдела." }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "Description", "Name", "Popularity" },
                values: new object[,]
                {
                    { 1, "Все, что связано с кодированием, языками программирования и разработкой ПО", "Программирование", 4 },
                    { 2, "Научные достижения, открытия и исследования", "Наука", 3 },
                    { 3, "Советы по здоровью, медицина и здоровый образ жизни", "Здоровье", 5 },
                    { 4, "Предпринимательство, управление и финансы", "Бизнес", 3 },
                    { 5, "Учеба, курсы, саморазвитие и обучение", "Образование", 2 }
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
                columns: new[] { "IssueId", "Information", "IsActive", "NewspaperId", "PublicationDate" },
                values: new object[,]
                {
                    { 1, "Новости Москвы", true, 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Обзор бизнеса", true, 2, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Новые технологии", true, 3, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Новости села", true, 4, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Искусство и культура", true, 5, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Научные открытия", true, 6, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Спортивные события", true, 7, new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Advertisements",
                columns: new[] { "AdvertisementId", "Cost", "IssueId", "Text", "Type" },
                values: new object[,]
                {
                    { 1, 100.50m, 1, "Скидка 50% на все товары! Только сегодня.", "Баннер" },
                    { 2, 200.00m, 1, "Подпишитесь на нашу рассылку и получите подарок!", "Всплывающее окно" },
                    { 3, 500.00m, 2, "Откройте для себя мир новых возможностей с нашим продуктом.", "Видео" },
                    { 4, 50.00m, 2, "Купите сейчас и получите бесплатную доставку.", "Текст" },
                    { 5, 120.00m, 3, "Лучшие предложения дня — не пропустите!", "Боковая панель" },
                    { 6, 1000.00m, 3, "Эксклюзивное предложение! Только для первых 100 клиентов.", "На всю страницу" },
                    { 7, 150.75m, 4, "Успейте купить! Акция заканчивается через 3 дня.", "Баннер" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "AuthorComment", "AuthorId", "CategoryId", "IssueId", "PublicationDate", "Text", "Title" },
                values: new object[,]
                {
                    { 1, "Эта статья посвящена эволюции и значению Dota 2 в киберспорте, её влиянию на индустрию и популярность среди игроков.", 1, 1, 1, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dota 2 — это одна из самых популярных многопользовательских онлайн-игр, разработанная и выпущенная компанией Valve. Игра является продолжением модификации Defense of the Ancients (DotA) для Warcraft III, которая в свою очередь была создана фанатами. В Dota 2 игроки управляют героями с уникальными способностями, сражаясь друг с другом в командных боях на аренах. С момента своего выпуска в 2013 году, игра приобрела огромную популярность благодаря своим глубоко проработанным механикам, регулярным обновлениям и мировой киберспортивной сцене. Чемпионаты, такие как The International, привлекли внимание зрителей со всего мира и сделали Dota 2 одной из самых зрелищных и высокооплачиваемых игр в истории киберспорта.", "Dota 2: История и эволюция игры" },
                    { 2, "Аниме Наруто вдохновляет многих зрителей, пропагандируя важность дружбы и силы духа в преодолении трудностей.", 2, 2, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Наруто — это культовое японское аниме, которое стало знаковым не только для поклонников японской культуры, но и для широкой аудитории по всему миру. История рассказывает о мальчике по имени Наруто Узумаке, который мечтает стать Хокаге, сильнейшим ниндзя своей деревни. Сюжет не только захватывает своим экшеном, но и затрагивает важные философские вопросы, такие как борьба за признание, ценность дружбы и преданности, а также то, что настоящая сила заключается не в физической мощи, а в решимости идти вперёд, несмотря на трудности. Аниме также прекрасно сочетает элементы боевых искусств и магии, что делает его одним из самых успешных проектов в истории японской анимации.", "Наруто: Путь Ниндзя и философия силы" },
                    { 3, "Статья поможет начинающим разработчикам разобраться в основах языка C# и его применении для различных проектов.", 3, 3, 2, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# (C-Sharp) — это объектно-ориентированный язык программирования, разработанный компанией Microsoft. Он является частью .NET Framework и используется для разработки различных приложений: от десктопных до мобильных и веб-приложений. C# сочетает в себе простоту и мощь, что делает его отличным выбором как для новичков, так и для опытных разработчиков. Основные особенности языка — это строгая типизация, поддержка параллельных вычислений, обработка исключений и богатая стандартная библиотека. C# активно используется в разработке игр (через Unity), а также в построении масштабируемых веб-приложений с использованием ASP.NET Core.", "C# для начинающих: Основы языка программирования" },
                    { 4, "React — отличная библиотека для создания гибких и производительных пользовательских интерфейсов в современных веб-приложениях.", 4, 1, 2, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "React — это библиотека JavaScript, разработанная Facebook для создания пользовательских интерфейсов. Она позволяет строить веб-приложения с динамическими и интерактивными интерфейсами, обновляя только те части страницы, которые изменяются. React использует концепцию виртуального DOM, что позволяет значительно улучшить производительность при изменении данных. Библиотека также поддерживает компонентный подход, где каждый элемент интерфейса является независимым компонентом, который можно повторно использовать и тестировать. React является популярным выбором для разработки сложных одностраничных приложений (SPA), и используется в таких крупных проектах, как Facebook, Instagram и WhatsApp.", "React: Введение в библиотеку для создания интерфейсов" },
                    { 5, "Unity — это отличная платформа для начинающих, которая позволяет легко создавать игры благодаря интуитивно понятному редактору и огромному сообществу разработчиков.", 5, 2, 3, new DateTime(2024, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unity — это одна из самых популярных платформ для разработки игр. С её помощью можно создавать игры как для мобильных устройств, так и для ПК, консолей и VR-устройств. Unity имеет мощный визуальный редактор, позволяющий быстро прототипировать игровые механики, а также использовать готовые ассеты и ресурсы из Unity Asset Store. Для создания игры не нужно быть опытным программистом — базовые знания о том, как работают игровые движки, и понимание принципов геймдизайна будут полезны для начала. Unity поддерживает множество языков программирования, но C# является основным для разработки в этом движке, что делает его отличным выбором для начинающих разработчиков игр.", "Как сделать свою первую игру с использованием Unity" },
                    { 6, "Counter-Strike 2 продолжает революционизировать многопользовательские шутеры, предлагая игрокам увлекательный и тактический опыт.", 6, 1, 3, new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Counter-Strike 2 — это продолжение культовой серии шутеров от первого лица, которая оказала значительное влияние на жанр многопользовательских игр. В новой версии, выпущенной после успешной первой игры и её многочисленных обновлений, игроки снова встают на одну из сторон — террористов или антитеррористов — и сражаются друг с другом в напряженных матчах. Главным новшеством является улучшенная графика и физика игры, а также новые карты, оружие и режимы. Counter-Strike 2 продолжает поддерживать активное сообщество игроков и киберспортивные турниры, такие как ESL и BLAST Premier, привлекая игроков со всего мира. Игра сочетает в себе динамичность, тактический подход и командную работу, что делает её одной из самых популярных многопользовательских игр в истории.", "Counter-Strike 2: Революция в игровом процессе" },
                    { 7, "Detroit: Become Human задаёт важные вопросы о будущем технологий и этике искусственного интеллекта, создавая уникальный опыт для игроков.", 7, 1, 4, new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Detroit: Become Human — это интерактивная драма и приключенческая игра, разработанная студией Quantic Dream. Игра разворачивается в будущем, где андроиды начинают обретать сознание и задаваться вопросами о своей роли в обществе. История фокусируется на трёх персонажах: андроиде-слуге Каре, андроиде-полицейском Конноре и андроиде-революционере Маркусе, которые оказываются втянутыми в борьбу за свободу и равенство. В игре принимаются важнейшие моральные решения, которые влияют на развитие событий, что делает каждый проход уникальным. Визуальный стиль игры впечатляет, а сюжет задаёт вопросы о природе человечности, моральных дилеммах и том, как мы относимся к технологиям, которые создаём.", "Detroit: Become Human — наше будущее будущее" }
                });

            migrationBuilder.InsertData(
                table: "ArticleTags",
                columns: new[] { "ArticleId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 1 },
                    { 4, 5 },
                    { 5, 3 },
                    { 6, 2 },
                    { 7, 4 }
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
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags",
                column: "TagId");

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
                name: "ArticleTags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Tags");

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
