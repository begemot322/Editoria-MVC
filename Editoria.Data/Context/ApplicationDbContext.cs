using Editoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Editoria.Data.Context
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {     
        }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DbSet<Author> Authors { get; set; } 

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

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Политика", Description = "Новости о политике" },
                new Category { CategoryId = 2, Name = "Экономика", Description = "Новости экономики и финансов" },
                new Category { CategoryId = 3, Name = "Культура", Description = "Новости о культуре и искусстве" },
                new Category { CategoryId = 4, Name = "Спорт", Description = "Спортивные события и новости" },
                new Category { CategoryId = 5, Name = "Технологии", Description = "Новости технологий и инноваций" },
                new Category { CategoryId = 6, Name = "Здоровье", Description = "Новости о здоровье и медицинских исследованиях" },
                new Category { CategoryId = 7, Name = "Образование", Description = "Новости образования и науки" }

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
                new Issue { IssueId = 1, PublicationDate = new DateTime(2024, 12, 1), Content = "Новости Москвы", NewspaperId = 1 },
                new Issue { IssueId = 2, PublicationDate = new DateTime(2024, 12, 2), Content = "Обзор бизнеса", NewspaperId = 2 },
                new Issue { IssueId = 3, PublicationDate = new DateTime(2024, 12, 3), Content = "Новые технологии", NewspaperId = 3 },
                new Issue { IssueId = 4, PublicationDate = new DateTime(2024, 12, 4), Content = "Новости села", NewspaperId = 4 },
                new Issue { IssueId = 5, PublicationDate = new DateTime(2024, 12, 5), Content = "Искусство и культура", NewspaperId = 5 },
                new Issue { IssueId = 6, PublicationDate = new DateTime(2024, 12, 6), Content = "Научные открытия", NewspaperId = 6 },
                new Issue { IssueId = 7, PublicationDate = new DateTime(2024, 12, 7), Content = "Спортивные события", NewspaperId = 7 }
            );
            modelBuilder.Entity<Article>().HasData(
                 new Article
                 {
                     ArticleId = 1,
                     Title = "Article 1",
                     Text = "Text of article 1",
                     PublicationDate = new DateTime(2024, 12, 13),
                     IssueId = 1,
                     CategoryId = 1,
                     AuthorId = 1
                 },
                 new Article
                 {
                     ArticleId = 2,
                     Title = "Article 2",
                     Text = "Text of article 2",
                     PublicationDate = new DateTime(2024, 12, 14),
                     IssueId = 1,
                     CategoryId = 2,
                     AuthorId = 2
                 },
                 new Article
                 {
                     ArticleId = 3,
                     Title = "Article 3",
                     Text = "Text of article 3",
                     PublicationDate = new DateTime(2024, 12, 15),
                     IssueId = 2,
                     CategoryId = 3,
                     AuthorId = 3
                 },
                 new Article
                 {
                     ArticleId = 4,
                     Title = "Article 4",
                     Text = "Text of article 4",
                     PublicationDate = new DateTime(2024, 12, 16),
                     IssueId = 2,
                     CategoryId = 1,
                     AuthorId = 4
                 },
                 new Article
                 {
                     ArticleId = 5,
                     Title = "Article 5",
                     Text = "Text of article 5",
                     PublicationDate = new DateTime(2024, 12, 17),
                     IssueId = 3,
                     CategoryId = 2,
                     AuthorId = 5
                 },
                 new Article
                 {
                     ArticleId = 6,
                     Title = "Article 6",
                     Text = "Text of article 6",
                     PublicationDate = new DateTime(2024, 12, 18),
                     IssueId = 3,
                     CategoryId = 3,
                     AuthorId = 6
                 },
                 new Article
                 {
                     ArticleId = 7,
                     Title = "Article 7",
                     Text = "Text of article 7",
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
                     Type = "Banner",
                     Text = "This is a banner advertisement.",
                     Cost = 100.50m,
                     IssueId = 1
                 },
                 new Advertisement
                 {
                     AdvertisementId = 2,
                     Type = "Popup",
                     Text = "This is a popup advertisement.",
                     Cost = 200.00m,
                     IssueId = 1
                 },
                 new Advertisement
                 {
                     AdvertisementId = 3,
                     Type = "Video",
                     Text = "This is a video advertisement.",
                     Cost = 500.00m,
                     IssueId = 2
                 },
                 new Advertisement
                 {
                     AdvertisementId = 4,
                     Type = "Text",
                     Text = "This is a text advertisement.",
                     Cost = 50.00m,
                     IssueId = 2
                 },
                 new Advertisement
                 {
                     AdvertisementId = 5,
                     Type = "Sidebar",
                     Text = "This is a sidebar advertisement.",
                     Cost = 120.00m,
                     IssueId = 3
                 },
                 new Advertisement
                 {
                     AdvertisementId = 6,
                     Type = "Full-page",
                     Text = "This is a full-page advertisement.",
                     Cost = 1000.00m,
                     IssueId = 3
                 },
                 new Advertisement
                 {
                     AdvertisementId = 7,
                     Type = "Banner",
                     Text = "Another banner advertisement.",
                     Cost = 150.75m,
                     IssueId = 4
                 }
             );


        }
    }
}
