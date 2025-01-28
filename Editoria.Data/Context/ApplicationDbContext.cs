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
                    Phone = "123-456-7890"
                },
                new Author
                {
                    AuthorId = 2,
                    Name = "Мария",
                    Surname = "Петрова",
                    Email = "maria.petrova@example.com",
                    Phone = "234-567-8901"
                },
                new Author
                {
                    AuthorId = 3,
                    Name = "Иван",
                    Surname = "Сидоров",
                    Email = "ivan.sidorov@example.com",
                    Phone = "345-678-9012"
                },
                new Author
                {
                    AuthorId = 4,
                    Name = "Елена",
                    Surname = "Кузнецова",
                    Email = "elena.kuznetsova@example.com",
                    Phone = "456-789-0123"
                },
                new Author
                {
                    AuthorId = 5,
                    Name = "Дмитрий",
                    Surname = "Смирнов",
                    Email = "dmitry.smirnov@example.com",
                    Phone = "567-890-1234"
                },
                new Author
                {
                    AuthorId = 6,
                    Name = "Ольга",
                    Surname = "Фёдорова",
                    Email = "olga.fedorova@example.com",
                    Phone = "678-901-2345"
                },
                new Author
                {
                    AuthorId = 7,
                    Name = "Сергей",
                    Surname = "Михайлов",
                    Email = "sergey.mikhailov@example.com",
                    Phone = "789-012-3456"
                }
            );

            modelBuilder.Entity<Editor>().HasData(
                new Editor { EditorId = 1, Name = "Анна", Surname = "Иванова", Email = "anna.ivanova@example.com" },
                new Editor { EditorId = 2, Name = "Борис", Surname = "Смирнов", Email = "boris.smirnov@example.com" },
                new Editor { EditorId = 3, Name = "Виктор", Surname = "Кузнецов", Email = "victor.kuznetsov@example.com" },
                new Editor { EditorId = 4, Name = "Галина", Surname = "Петрова", Email = "galina.petrova@example.com" },
                new Editor { EditorId = 5, Name = "Дмитрий", Surname = "Сидоров", Email = "dmitriy.sidorov@example.com" },
                new Editor { EditorId = 6, Name = "Екатерина", Surname = "Соболева", Email = "ekaterina.soboleva@example.com" },
                new Editor { EditorId = 7, Name = "Жанна", Surname = "Николаева", Email = "zhanna.nikolaeva@example.com" }
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
                     AuthorId = 1
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
                     AuthorId = 2
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
                     AuthorId = 3
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
                     AuthorId = 4
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
                     AuthorId = 5
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
                     AuthorId = 6
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
                     AuthorId = 7
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
                new Tag { TagId = 1, Name = "Программирование", Description = "Все, что связано с кодированием, языками программирования и разработкой ПО" },
                new Tag { TagId = 2, Name = "Наука", Description = "Научные достижения, открытия и исследования" },
                new Tag { TagId = 3, Name = "Здоровье", Description = "Советы по здоровью, медицина и здоровый образ жизни" },
                new Tag { TagId = 4, Name = "Бизнес", Description = "Предпринимательство, управление и финансы" },
                new Tag { TagId = 5, Name = "Образование", Description = "Учеба, курсы, саморазвитие и обучение" }
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
