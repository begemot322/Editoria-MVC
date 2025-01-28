using Editoria.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Editoria.Data.Context
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {     
        }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Editor>()
                .HasOne(e => e.Newspaper)
                .WithOne(n=>n.Editor)
                .HasForeignKey<Newspaper>(e => e.EditorId);

            modelBuilder.Entity<Issue>()
                .HasOne(i=>i.Newspaper)
                .WithMany(n=>n.Issues)
                .HasForeignKey(i=>i.NewspaperId);

            modelBuilder.Entity<Article>()
                .HasOne(a=>a.Issue)
                .WithMany(a=>a.Articles)
                .HasForeignKey(i=>i.IssueId);

            modelBuilder.Entity<Article>()
                .HasOne(a=>a.Category)
                .WithMany(c=>c.Articles)
                .HasForeignKey(a=>a.CategoryId);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(a=>a.Articles)
                .HasForeignKey(a => a.AuthorId);

            modelBuilder.Entity<Advertisement>()
               .HasOne(a => a.Issue)
               .WithMany(i => i.Advertisements)
               .HasForeignKey(a => a.IssueId);

            modelBuilder.Entity<ArticleTag>()
                .HasKey(at => new { at.ArticleId, at.TagId }); 
            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleId);
            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagId);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Политика", Description = "Новости о политике", IsActive = true, Priority = 5 },
                new Category { CategoryId = 2, Name = "Экономика", Description = "Новости экономики и финансов", IsActive = true, Priority = 5 },
                new Category { CategoryId = 3, Name = "Культура", Description = "Новости о культуре и искусстве", IsActive = true, Priority = 3 },
                new Category { CategoryId = 4, Name = "Спорт", Description = "Спортивные события и новости", IsActive = true, Priority = 4 },
                new Category { CategoryId = 5, Name = "Технологии", Description = "Новости технологий и инноваций", IsActive = true, Priority = 4 },
                new Category { CategoryId = 6, Name = "Здоровье", Description = "Новости о здоровье и медицинских исследованиях", IsActive = true, Priority = 3 },
                new Category { CategoryId = 7, Name = "Образование", Description = "Новости образования и науки", IsActive = true, Priority = 2 },
                new Category { CategoryId = 8, Name = "Киберспорт", Description = "Новости о киберспорте", IsActive = false, Priority = 1 } 
            );

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = 1,
                    Name = "Алексей",
                    Surname = "Иванов",
                    Email = "alexey.ivanov@example.com",
                    Phone = "123-456-7890",
                    Biography = "Алексей Иванов — опытный журналист с более чем 10-летним стажем работы в различных СМИ. " +
                    "Он освещал важные политические события, а также занимается аналитическими расследованиями." +
                    " Алексей — автор нескольких книг и статей, которые стали знаковыми в своей области.",
                    DateOfBirth = new DateTime(1980, 5, 15),
                    WorkExperience = "Работа в газете 'Вести' (2008-2015), журнал 'Мир новостей' (2016-2020), автор статей на различные темы."
                },
                new Author
                {
                    AuthorId = 2,
                    Name = "Мария",
                    Surname = "Петрова",
                    Email = "maria.petrova@example.com",
                    Phone = "234-567-8901",
                    Biography = "Мария Петрова — редактор с сильными навыками работы с контентом и журналистскими расследованиями." +
                    " Она активно сотрудничает с ведущими СМИ и ведет образовательные курсы по редактированию. Мария также является" +
                    " экспертом в области цифровых медиа и новых форматов журналистики.",
                    DateOfBirth = new DateTime(1990, 3, 22),
                    WorkExperience = "Опыт работы в интернет-издании 'ТехноНовости' (2015-2018), автор статей в журнале 'Бизнес и технологии' (2019-2021)."
                },
                new Author  
                {
                    AuthorId = 3,
                    Name = "Иван",
                    Surname = "Сидоров",
                    Email = "ivan.sidorov@example.com",
                    Phone = "345-678-9012",
                    Biography = "Иван Сидоров — опытный редактор с фокусом на научные и технические публикации." +
                    " Он работает с высококвалифицированными экспертами в области технологий и науки, обеспечивая " +
                    "точность и ясность материалов. Иван также активно участвует в редакторских советах научных журналов и конференций.",
                    DateOfBirth = new DateTime(1985, 7, 30),
                    WorkExperience = "Работа в журнале 'Наука и технологии' (2010-2017), редактор в научном отделе газеты 'Техник'."
                },
                new Author
                {
                    AuthorId = 4,
                    Name = "Елена",
                    Surname = "Кузнецова",
                    Email = "elena.kuznetsova@example.com",
                    Phone = "456-789-0123",
                    Biography = "Елена Кузнецова — журналист и редактор, специализирующаяся на культурных событиях и " +
                    "социальных вопросах. Она активно освещает темы, связанные с современным искусством, общественными" +
                    " движениями и правами человека. Елена также организует общественные проекты, направленные на развитие " +
                    "культурных инициатив.",
                    DateOfBirth = new DateTime(1992, 11, 2),
                    WorkExperience = "Опыт работы в журнале 'Культура и искусство' (2014-2018), автор статей в новостном портале 'Свет культуры'."
                },
                new Author
                {
                    AuthorId = 5,
                    Name = "Дмитрий",
                    Surname = "Смирнов",
                    Email = "dmitry.smirnov@example.com",
                    Phone = "567-890-1234",
                    Biography = "Дмитрий Смирнов — редактор с опытом работы в новостных и политических изданиях. " +
                    "Он специализируется на анализе политических событий и подготовке репортажей с горячих точек. " +
                    "Дмитрий также активно работает с экспертами и политическими аналитиками для создания глубоких и" +
                    " объективных материалов.",
                    DateOfBirth = new DateTime(1988, 9, 18),
                    WorkExperience = "Работа в газете 'Политика сегодня' (2012-2017), редактор в новостном агентстве 'Мир сегодня'."
                },
                new Author
                {
                    AuthorId = 6,
                    Name = "Ольга",
                    Surname = "Фёдорова",
                    Email = "olga.fedorova@example.com",
                    Phone = "678-901-2345",
                    Biography = "Ольга Фёдорова — начинающий редактор с большим потенциалом и интересом к культурным " +
                    "и образовательным темам. Она активно участвует в создании контента, освещающего важные события в " +
                    "сфере образования и культуры. Ольга также стремится развивать свои навыки в области журналистских" +
                    " расследований и аналитики.",
                    DateOfBirth = new DateTime(1995, 1, 25),
                    WorkExperience = "Работа в стартап-издании 'Культурная эволюция' (2017-2020)."
                },
                new Author
                {
                    AuthorId = 7,
                    Name = "Сергей",
                    Surname = "Михайлов",
                    Email = "sergey.mikhailov@example.com",
                    Phone = "789-012-3456",
                    Biography = "Сергей Михайлов — эксперт по экономическим и политическим темам, работающий в" +
                    " крупных новостных агентствах. Он анализирует ключевые экономические тренды и политические процессы, " +
                    "предлагая экспертные оценки и прогнозы. Сергей также ведет лекции и семинары для профессионалов в " +
                    "области журналистики и аналитики.",
                    DateOfBirth = new DateTime(1983, 12, 10),
                    WorkExperience = "Работа в агентстве 'Новости России' (2009-2014), автор статей в международном новостном агентстве 'World News'."
                }
            );

            modelBuilder.Entity<Editor>().HasData(
                new Editor
                {
                    EditorId = 1,
                    Name = "Анна",
                    Surname = "Иванова",
                    Email = "anna.ivanova@example.com",
                    Phone = "123-456-7890",
                    Biography = "Анна Иванова — опытный редактор с многолетним стажем работы в различных изданиях. " +
                                "Она специализируется на редактуре материалов по вопросам политики и международных отношений. " +
                                "Анна участвовала в создании множества успешных проектов и редакционных материалов, известных на региональном уровне.",
                    DateOfBirth = new DateTime(1985, 7, 15),
                    WorkExperience = "Работа в редакции газеты 'Молодёжный вестник' (2008-2015), в журнале 'Женская точка зрения' (2016-2021)."
                },
                new Editor
                {
                    EditorId = 2,
                    Name = "Борис",
                    Surname = "Смирнов",
                    Email = "boris.smirnov@example.com",
                    Phone = "234-567-8901",
                    Biography = "Борис Смирнов — опытный редактор в области политики и экономики. " +
                                "Он работал в крупнейших политических изданиях, освещая важнейшие события в экономике и политике. " +
                                "Борис также автор ряда аналитических статей и материалов, которые стали основой для дискуссий в экспертных кругах.",
                    DateOfBirth = new DateTime(1980, 4, 22),
                    WorkExperience = "Опыт работы в газете 'Политика сегодня' (2006-2015), редактор в журнале 'Экономика и бизнес' (2016-2020)."
                },
                new Editor
                {
                    EditorId = 3,
                    Name = "Виктор",
                    Surname = "Кузнецов",
                    Email = "victor.kuznetsov@example.com",
                    Phone = "345-678-9012",
                    Biography = "Виктор Кузнецов — редактор с опытом работы в области культуры и искусства. " +
                                "Он является экспертам в области театра и изобразительного искусства, и его статьи об этих темах пользовались большой популярностью." +
                                " Виктор также активно участвует в культурных проектах и организует мероприятия для художников и критиков.",
                    DateOfBirth = new DateTime(1990, 11, 5),
                    WorkExperience = "Работа в журнале 'Культура и искусство' (2012-2018), редактор в культурном отделе газеты 'Новости культуры'."
                },
                new Editor
                {
                    EditorId = 4,
                    Name = "Галина",
                    Surname = "Петрова",
                    Email = "galina.petrova@example.com",
                    Phone = "456-789-0123",
                    Biography = "Галина Петрова — редактор с опытом работы в сфере здравоохранения и экологии. " +
                                "Она специализируется на освещении экологических проблем и вопросов здравоохранения в современных условиях." +
                                " Галина активно занимается развитием экологических программ и общественных инициатив в сфере охраны природы.",
                    DateOfBirth = new DateTime(1987, 2, 19),
                    WorkExperience = "Работа в журнале 'Зеленая планета' (2010-2016), редактор в отделе экологии газеты 'Здоровье и жизнь'."
                },
                new Editor
                {
                    EditorId = 5,
                    Name = "Дмитрий",
                    Surname = "Сидоров",
                    Email = "dmitriy.sidorov@example.com",
                    Phone = "567-890-1234",
                    Biography = "Дмитрий Сидоров — журналист и редактор с многолетним стажем работы в области науки. " +
                                "Он активно участвует в популяризации науки и технологий, освещая самые актуальные достижения в этих областях." +
                                " Дмитрий является автором многочисленных научных публикаций и работ в области информатики и медицины.",
                    DateOfBirth = new DateTime(1984, 9, 10),
                    WorkExperience = "Опыт работы в журнале 'Наука и технологии' (2009-2017), редактор в новостном агентстве 'Мир науки'."
                },
                new Editor
                {
                    EditorId = 6,
                    Name = "Екатерина",
                    Surname = "Соболева",
                    Email = "ekaterina.soboleva@example.com",
                    Phone = "678-901-2345",
                    Biography = "Екатерина Соболева — молодой редактор с интересом к образовательной тематике. " +
                                "Она увлечена развитием цифрового образования и освещением новых подходов в обучении." +
                                " Екатерина активно работает с образовательными проектами и занимается разработкой курсов для онлайн-образования.",
                    DateOfBirth = new DateTime(1993, 8, 13),
                    WorkExperience = "Работа в онлайн-издании 'Образование будущего' (2015-2019). Начинающий редактор в проекте 'Образование и технологии'."
                },
                new Editor
                {
                    EditorId = 7,
                    Name = "Жанна",
                    Surname = "Николаева",
                    Email = "zhanna.nikolaeva@example.com",
                    Phone = "789-012-3456",
                    Biography = "Жанна Николаева — редактор и журналист, специализирующаяся на новостях о жизни города. " +
                                "Она известна своими репортажами о социальных проблемах и городских инициативах." +
                                " Жанна активно работает с городской администрацией и местными НКО для решения актуальных вопросов жизни в городе.",
                    DateOfBirth = new DateTime(1991, 1, 30),
                    WorkExperience = "Работа в газете 'Городские новости' (2014-2019), редактор новостного отдела."
                }
            );

            modelBuilder.Entity<Newspaper>().HasData(
                new Newspaper { NewspaperId = 1, Name = "Городские новости", Circulation = 10000, Type = "Ежедневная", Region = "Москва", EditorId = 1 },
                new Newspaper { NewspaperId = 2, Name = "Бизнес-обозрение", Circulation = 5000, Type = "Еженедельная", Region = "Санкт-Петербург", EditorId = 2 },
                new Newspaper { NewspaperId = 3, Name = "Технологический вестник", Circulation = 7000, Type = "Ежемесячная", Region = "Новосибирск", EditorId = 3 },
                new Newspaper { NewspaperId = 4, Name = "Сельская жизнь", Circulation = 3000, Type = "Еженедельная", Region = "Краснодар", EditorId = 4 },
                new Newspaper { NewspaperId = 5, Name = "Культура и искусство", Circulation = 4000, Type = "Ежемесячная", Region = "Казань", EditorId = 5 },
                new Newspaper { NewspaperId = 6, Name = "Наука сегодня", Circulation = 6000, Type = "Ежемесячная", Region = "Екатеринбург", EditorId = 6 },
                new Newspaper { NewspaperId = 7, Name = "Мир спорта", Circulation = 8000, Type = "Еженедельная", Region = "Сочи", EditorId = 7 }
            );

            modelBuilder.Entity<Issue>().HasData(
                new Issue { IssueId = 1, PublicationDate = new DateTime(2024, 12, 1), Information = "Новости Москвы", NewspaperId = 1, IsActive = true },
                new Issue { IssueId = 2, PublicationDate = new DateTime(2024, 12, 2), Information = "Обзор бизнеса", NewspaperId = 2, IsActive = true },
                new Issue { IssueId = 3, PublicationDate = new DateTime(2024, 12, 3), Information = "Новые технологии", NewspaperId = 3, IsActive = true },
                new Issue { IssueId = 4, PublicationDate = new DateTime(2024, 12, 4), Information = "Новости села", NewspaperId = 4, IsActive = true },
                new Issue { IssueId = 5, PublicationDate = new DateTime(2024, 12, 5), Information = "Искусство и культура", NewspaperId = 5, IsActive = true },
                new Issue { IssueId = 6, PublicationDate = new DateTime(2024, 12, 6), Information = "Научные открытия", NewspaperId = 6, IsActive = true },
                new Issue { IssueId = 7, PublicationDate = new DateTime(2024, 12, 7), Information = "Спортивные события", NewspaperId = 7, IsActive = true }
            );

            modelBuilder.Entity<Article>().HasData(
                new Article
                {
                    ArticleId = 1,
                    Title = "Dota 2: История и эволюция игры",
                    Text = "Dota 2 — это одна из самых популярных многопользовательских онлайн-игр, разработанная и выпущенная компанией Valve. Игра является продолжением модификации Defense of the Ancients (DotA) для Warcraft III, которая в свою очередь была создана фанатами. В Dota 2 игроки управляют героями с уникальными способностями, сражаясь друг с другом в командных боях на аренах. " +
                        "С момента своего выпуска в 2013 году, игра приобрела огромную популярность благодаря своим глубоко проработанным механикам, регулярным обновлениям и мировой киберспортивной сцене. Чемпионаты, такие как The International, привлекли внимание зрителей со всего мира и сделали Dota 2 одной из самых зрелищных и высокооплачиваемых игр в истории киберспорта.",
                    PublicationDate = new DateTime(2024, 12, 13),
                    IssueId = 1,
                    CategoryId = 1,
                    AuthorId = 1,
                    AuthorComment = "Эта статья посвящена эволюции и значению Dota 2 в киберспорте, её влиянию на индустрию и популярность среди игроков."
                },
                new Article
                {
                    ArticleId = 2,
                    Title = "Наруто: Путь Ниндзя и философия силы",
                    Text = "Наруто — это культовое японское аниме, которое стало знаковым не только для поклонников японской культуры, но и для широкой аудитории по всему миру. История рассказывает о мальчике по имени Наруто Узумаке, который мечтает стать Хокаге, сильнейшим ниндзя своей деревни. " +
                        "Сюжет не только захватывает своим экшеном, но и затрагивает важные философские вопросы, такие как борьба за признание, ценность дружбы и преданности, а также то, что настоящая сила заключается не в физической мощи, а в решимости идти вперёд, несмотря на трудности. Аниме также прекрасно сочетает элементы боевых искусств и магии, что делает его одним из самых успешных проектов в истории японской анимации.",
                    PublicationDate = new DateTime(2024, 12, 14),
                    IssueId = 1,
                    CategoryId = 2,
                    AuthorId = 2,
                    AuthorComment = "Аниме Наруто вдохновляет многих зрителей, пропагандируя важность дружбы и силы духа в преодолении трудностей."
                },
                new Article
                {
                    ArticleId = 3,
                    Title = "C# для начинающих: Основы языка программирования",
                    Text = "C# (C-Sharp) — это объектно-ориентированный язык программирования, разработанный компанией Microsoft. Он является частью .NET Framework и используется для разработки различных приложений: от десктопных до мобильных и веб-приложений. C# сочетает в себе простоту и мощь, что делает его отличным выбором как для новичков, так и для опытных разработчиков. " +
                        "Основные особенности языка — это строгая типизация, поддержка параллельных вычислений, обработка исключений и богатая стандартная библиотека. C# активно используется в разработке игр (через Unity), а также в построении масштабируемых веб-приложений с использованием ASP.NET Core.",
                    PublicationDate = new DateTime(2024, 12, 15),
                    IssueId = 2,
                    CategoryId = 3,
                    AuthorId = 3,
                    AuthorComment = "Статья поможет начинающим разработчикам разобраться в основах языка C# и его применении для различных проектов."
                },
                new Article
                {
                    ArticleId = 4,
                    Title = "React: Введение в библиотеку для создания интерфейсов",
                    Text = "React — это библиотека JavaScript, разработанная Facebook для создания пользовательских интерфейсов. Она позволяет строить веб-приложения с динамическими и интерактивными интерфейсами, обновляя только те части страницы, которые изменяются. React использует концепцию виртуального DOM, что позволяет значительно улучшить производительность при изменении данных. " +
                        "Библиотека также поддерживает компонентный подход, где каждый элемент интерфейса является независимым компонентом, который можно повторно использовать и тестировать. React является популярным выбором для разработки сложных одностраничных приложений (SPA), и используется в таких крупных проектах, как Facebook, Instagram и WhatsApp.",
                    PublicationDate = new DateTime(2024, 12, 16),
                    IssueId = 2,
                    CategoryId = 1,
                    AuthorId = 4,
                    AuthorComment = "React — отличная библиотека для создания гибких и производительных пользовательских интерфейсов в современных веб-приложениях."
                },
                new Article
                {
                    ArticleId = 5,
                    Title = "Как сделать свою первую игру с использованием Unity",
                    Text = "Unity — это одна из самых популярных платформ для разработки игр. С её помощью можно создавать игры как для мобильных устройств, так и для ПК, консолей и VR-устройств. Unity имеет мощный визуальный редактор, позволяющий быстро прототипировать игровые механики, а также использовать готовые ассеты и ресурсы из Unity Asset Store. " +
                        "Для создания игры не нужно быть опытным программистом — базовые знания о том, как работают игровые движки, и понимание принципов геймдизайна будут полезны для начала. Unity поддерживает множество языков программирования, но C# является основным для разработки в этом движке, что делает его отличным выбором для начинающих разработчиков игр.",
                    PublicationDate = new DateTime(2024, 12, 17),
                    IssueId = 3,
                    CategoryId = 2,
                    AuthorId = 5,
                    AuthorComment = "Unity — это отличная платформа для начинающих, которая позволяет легко создавать игры благодаря интуитивно понятному редактору и огромному сообществу разработчиков."
                },
                new Article
                {
                    ArticleId = 6,
                    Title = "Counter-Strike 2: Революция в игровом процессе",
                    Text = "Counter-Strike 2 — это продолжение культовой серии шутеров от первого лица, которая оказала значительное влияние на жанр многопользовательских игр. В новой версии, выпущенной после успешной первой игры и её многочисленных обновлений, игроки снова встают на одну из сторон — террористов или антитеррористов — и сражаются друг с другом в напряженных матчах. " +
                        "Главным новшеством является улучшенная графика и физика игры, а также новые карты, оружие и режимы. Counter-Strike 2 продолжает поддерживать активное сообщество игроков и киберспортивные турниры, такие как ESL и BLAST Premier, привлекая игроков со всего мира. Игра сочетает в себе динамичность, тактический подход и командную работу, что делает её одной из самых популярных многопользовательских игр в истории.",
                    PublicationDate = new DateTime(2024, 12, 18),
                    IssueId = 3,
                    CategoryId = 1,
                    AuthorId = 6,
                    AuthorComment = "Counter-Strike 2 продолжает революционизировать многопользовательские шутеры, предлагая игрокам увлекательный и тактический опыт."
                },
                new Article
                {
                    ArticleId = 7,
                    Title = "Detroit: Become Human — наше будущее будущее",
                    Text = "Detroit: Become Human — это интерактивная драма и приключенческая игра, разработанная студией Quantic Dream. Игра разворачивается в будущем, где андроиды начинают обретать сознание и задаваться вопросами о своей роли в обществе. История фокусируется на трёх персонажах: андроиде-слуге Каре, андроиде-полицейском Конноре и андроиде-революционере Маркусе, которые оказываются втянутыми в борьбу за свободу и равенство. " +
                        "В игре принимаются важнейшие моральные решения, которые влияют на развитие событий, что делает каждый проход уникальным. Визуальный стиль игры впечатляет, а сюжет задаёт вопросы о природе человечности, моральных дилеммах и том, как мы относимся к технологиям, которые создаём.",
                    PublicationDate = new DateTime(2024, 12, 19),
                    IssueId = 4,
                    CategoryId = 1,
                    AuthorId = 7,
                    AuthorComment = "Detroit: Become Human задаёт важные вопросы о будущем технологий и этике искусственного интеллекта, создавая уникальный опыт для игроков."
                }
            );

            modelBuilder.Entity<Advertisement>().HasData(
                 new Advertisement
                 {
                     AdvertisementId = 1,
                     Type = "Баннер",
                     Text = "Скидка 50% на все товары! Только сегодня.",
                     Cost = 100.50m,
                     IssueId = 1
                 },
                 new Advertisement
                 {
                     AdvertisementId = 2,
                     Type = "Всплывающее окно",
                     Text = "Подпишитесь на нашу рассылку и получите подарок!",
                     Cost = 200.00m,
                     IssueId = 1
                 },
                 new Advertisement
                 {
                     AdvertisementId = 3,
                     Type = "Видео",
                     Text = "Откройте для себя мир новых возможностей с нашим продуктом.",
                     Cost = 500.00m,
                     IssueId = 2
                 },
                 new Advertisement
                 {
                     AdvertisementId = 4,
                     Type = "Текст",
                     Text = "Купите сейчас и получите бесплатную доставку.",
                     Cost = 50.00m,
                     IssueId = 2
                 },
                 new Advertisement
                 {
                     AdvertisementId = 5,
                     Type = "Боковая панель",
                     Text = "Лучшие предложения дня — не пропустите!",
                     Cost = 120.00m,
                     IssueId = 3
                 },
                 new Advertisement
                 {
                     AdvertisementId = 6,
                     Type = "На всю страницу",
                     Text = "Эксклюзивное предложение! Только для первых 100 клиентов.",
                     Cost = 1000.00m,
                     IssueId = 3
                 },
                 new Advertisement
                 {
                     AdvertisementId = 7,
                     Type = "Баннер",
                     Text = "Успейте купить! Акция заканчивается через 3 дня.",
                     Cost = 150.75m,
                     IssueId = 4
                 }
             );

            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "Программирование", Description = "Все, что связано с кодированием, языками программирования и разработкой ПО", Popularity = 4 },
                new Tag { TagId = 2, Name = "Наука", Description = "Научные достижения, открытия и исследования", Popularity =3 },
                new Tag { TagId = 3, Name = "Здоровье", Description = "Советы по здоровью, медицина и здоровый образ жизни", Popularity = 5 },
                new Tag { TagId = 4, Name = "Бизнес", Description = "Предпринимательство, управление и финансы", Popularity = 3 },
                new Tag { TagId = 5, Name = "Образование", Description = "Учеба, курсы, саморазвитие и обучение", Popularity = 2 }
            );

            modelBuilder.Entity<ArticleTag>().HasData(
                new ArticleTag { ArticleId = 1, TagId = 1 },
                new ArticleTag { ArticleId = 1, TagId = 2 },
                new ArticleTag { ArticleId = 2, TagId = 3 },
                new ArticleTag { ArticleId = 3, TagId = 4 }, 
                new ArticleTag { ArticleId = 4, TagId = 1 }, 
                new ArticleTag { ArticleId = 4, TagId = 5 },
                new ArticleTag { ArticleId = 5, TagId = 3 },
                new ArticleTag { ArticleId = 6, TagId = 2 },
                new ArticleTag { ArticleId = 7, TagId = 4 } 
            );



        }
    }
}
