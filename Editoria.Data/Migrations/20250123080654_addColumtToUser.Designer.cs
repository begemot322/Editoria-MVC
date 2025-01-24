﻿// <auto-generated />
using System;
using Editoria.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Editoria.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250123080654_addColumtToUser")]
    partial class addColumtToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Editoria.Models.Entities.Advertisement", b =>
                {
                    b.Property<int>("AdvertisementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdvertisementId"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AdvertisementId");

                    b.HasIndex("IssueId");

                    b.ToTable("Advertisements");

                    b.HasData(
                        new
                        {
                            AdvertisementId = 1,
                            Cost = 100.50m,
                            IssueId = 1,
                            Text = "This is a banner advertisement.",
                            Type = "Banner"
                        },
                        new
                        {
                            AdvertisementId = 2,
                            Cost = 200.00m,
                            IssueId = 1,
                            Text = "This is a popup advertisement.",
                            Type = "Popup"
                        },
                        new
                        {
                            AdvertisementId = 3,
                            Cost = 500.00m,
                            IssueId = 2,
                            Text = "This is a video advertisement.",
                            Type = "Video"
                        },
                        new
                        {
                            AdvertisementId = 4,
                            Cost = 50.00m,
                            IssueId = 2,
                            Text = "This is a text advertisement.",
                            Type = "Text"
                        },
                        new
                        {
                            AdvertisementId = 5,
                            Cost = 120.00m,
                            IssueId = 3,
                            Text = "This is a sidebar advertisement.",
                            Type = "Sidebar"
                        },
                        new
                        {
                            AdvertisementId = 6,
                            Cost = 1000.00m,
                            IssueId = 3,
                            Text = "This is a full-page advertisement.",
                            Type = "Full-page"
                        },
                        new
                        {
                            AdvertisementId = 7,
                            Cost = 150.75m,
                            IssueId = 4,
                            Text = "Another banner advertisement.",
                            Type = "Banner"
                        });
                });

            modelBuilder.Entity("Editoria.Models.Entities.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ArticleId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("IssueId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            ArticleId = 1,
                            AuthorId = 1,
                            CategoryId = 1,
                            IssueId = 1,
                            PublicationDate = new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Text of article 1",
                            Title = "Article 1"
                        },
                        new
                        {
                            ArticleId = 2,
                            AuthorId = 2,
                            CategoryId = 2,
                            IssueId = 1,
                            PublicationDate = new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Text of article 2",
                            Title = "Article 2"
                        },
                        new
                        {
                            ArticleId = 3,
                            AuthorId = 3,
                            CategoryId = 3,
                            IssueId = 2,
                            PublicationDate = new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Text of article 3",
                            Title = "Article 3"
                        },
                        new
                        {
                            ArticleId = 4,
                            AuthorId = 4,
                            CategoryId = 1,
                            IssueId = 2,
                            PublicationDate = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Text of article 4",
                            Title = "Article 4"
                        },
                        new
                        {
                            ArticleId = 5,
                            AuthorId = 5,
                            CategoryId = 2,
                            IssueId = 3,
                            PublicationDate = new DateTime(2024, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Text of article 5",
                            Title = "Article 5"
                        },
                        new
                        {
                            ArticleId = 6,
                            AuthorId = 6,
                            CategoryId = 3,
                            IssueId = 3,
                            PublicationDate = new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Text of article 6",
                            Title = "Article 6"
                        },
                        new
                        {
                            ArticleId = 7,
                            AuthorId = 7,
                            CategoryId = 1,
                            IssueId = 4,
                            PublicationDate = new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Text of article 7",
                            Title = "Article 7"
                        });
                });

            modelBuilder.Entity("Editoria.Models.Entities.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            Email = "alexey.ivanov@example.com",
                            Name = "Алексей",
                            Phone = "123-456-7890",
                            Surname = "Иванов"
                        },
                        new
                        {
                            AuthorId = 2,
                            Email = "maria.petrova@example.com",
                            Name = "Мария",
                            Phone = "234-567-8901",
                            Surname = "Петрова"
                        },
                        new
                        {
                            AuthorId = 3,
                            Email = "ivan.sidorov@example.com",
                            Name = "Иван",
                            Phone = "345-678-9012",
                            Surname = "Сидоров"
                        },
                        new
                        {
                            AuthorId = 4,
                            Email = "elena.kuznetsova@example.com",
                            Name = "Елена",
                            Phone = "456-789-0123",
                            Surname = "Кузнецова"
                        },
                        new
                        {
                            AuthorId = 5,
                            Email = "dmitry.smirnov@example.com",
                            Name = "Дмитрий",
                            Phone = "567-890-1234",
                            Surname = "Смирнов"
                        },
                        new
                        {
                            AuthorId = 6,
                            Email = "olga.fedorova@example.com",
                            Name = "Ольга",
                            Phone = "678-901-2345",
                            Surname = "Фёдорова"
                        },
                        new
                        {
                            AuthorId = 7,
                            Email = "sergey.mikhailov@example.com",
                            Name = "Сергей",
                            Phone = "789-012-3456",
                            Surname = "Михайлов"
                        });
                });

            modelBuilder.Entity("Editoria.Models.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Новости о политике",
                            Name = "Политика"
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Новости экономики и финансов",
                            Name = "Экономика"
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "Новости о культуре и искусстве",
                            Name = "Культура"
                        },
                        new
                        {
                            CategoryId = 4,
                            Description = "Спортивные события и новости",
                            Name = "Спорт"
                        },
                        new
                        {
                            CategoryId = 5,
                            Description = "Новости технологий и инноваций",
                            Name = "Технологии"
                        },
                        new
                        {
                            CategoryId = 6,
                            Description = "Новости о здоровье и медицинских исследованиях",
                            Name = "Здоровье"
                        },
                        new
                        {
                            CategoryId = 7,
                            Description = "Новости образования и науки",
                            Name = "Образование"
                        });
                });

            modelBuilder.Entity("Editoria.Models.Entities.Editor", b =>
                {
                    b.Property<int>("EditorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EditorId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EditorId");

                    b.ToTable("Editors");

                    b.HasData(
                        new
                        {
                            EditorId = 1,
                            Email = "anna.ivanova@example.com",
                            Name = "Анна",
                            Surname = "Иванова"
                        },
                        new
                        {
                            EditorId = 2,
                            Email = "boris.smirnov@example.com",
                            Name = "Борис",
                            Surname = "Смирнов"
                        },
                        new
                        {
                            EditorId = 3,
                            Email = "victor.kuznetsov@example.com",
                            Name = "Виктор",
                            Surname = "Кузнецов"
                        },
                        new
                        {
                            EditorId = 4,
                            Email = "galina.petrova@example.com",
                            Name = "Галина",
                            Surname = "Петрова"
                        },
                        new
                        {
                            EditorId = 5,
                            Email = "dmitriy.sidorov@example.com",
                            Name = "Дмитрий",
                            Surname = "Сидоров"
                        },
                        new
                        {
                            EditorId = 6,
                            Email = "ekaterina.soboleva@example.com",
                            Name = "Екатерина",
                            Surname = "Соболева"
                        },
                        new
                        {
                            EditorId = 7,
                            Email = "zhanna.nikolaeva@example.com",
                            Name = "Жанна",
                            Surname = "Николаева"
                        });
                });

            modelBuilder.Entity("Editoria.Models.Entities.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("NewspaperId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IssueId");

                    b.HasIndex("NewspaperId");

                    b.ToTable("Issues");

                    b.HasData(
                        new
                        {
                            IssueId = 1,
                            Content = "Новости Москвы",
                            NewspaperId = 1,
                            PublicationDate = new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IssueId = 2,
                            Content = "Обзор бизнеса",
                            NewspaperId = 2,
                            PublicationDate = new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IssueId = 3,
                            Content = "Новые технологии",
                            NewspaperId = 3,
                            PublicationDate = new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IssueId = 4,
                            Content = "Новости села",
                            NewspaperId = 4,
                            PublicationDate = new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IssueId = 5,
                            Content = "Искусство и культура",
                            NewspaperId = 5,
                            PublicationDate = new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IssueId = 6,
                            Content = "Научные открытия",
                            NewspaperId = 6,
                            PublicationDate = new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IssueId = 7,
                            Content = "Спортивные события",
                            NewspaperId = 7,
                            PublicationDate = new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Editoria.Models.Entities.Newspaper", b =>
                {
                    b.Property<int>("NewspaperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewspaperId"));

                    b.Property<int>("Circulation")
                        .HasColumnType("int");

                    b.Property<int>("EditorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewspaperId");

                    b.HasIndex("EditorId")
                        .IsUnique();

                    b.ToTable("Newspapers");

                    b.HasData(
                        new
                        {
                            NewspaperId = 1,
                            Circulation = 10000,
                            EditorId = 1,
                            Name = "Городские новости",
                            Region = "Москва",
                            Type = "Ежедневная"
                        },
                        new
                        {
                            NewspaperId = 2,
                            Circulation = 5000,
                            EditorId = 2,
                            Name = "Бизнес-обозрение",
                            Region = "Санкт-Петербург",
                            Type = "Еженедельная"
                        },
                        new
                        {
                            NewspaperId = 3,
                            Circulation = 7000,
                            EditorId = 3,
                            Name = "Технологический вестник",
                            Region = "Новосибирск",
                            Type = "Ежемесячная"
                        },
                        new
                        {
                            NewspaperId = 4,
                            Circulation = 3000,
                            EditorId = 4,
                            Name = "Сельская жизнь",
                            Region = "Краснодар",
                            Type = "Еженедельная"
                        },
                        new
                        {
                            NewspaperId = 5,
                            Circulation = 4000,
                            EditorId = 5,
                            Name = "Культура и искусство",
                            Region = "Казань",
                            Type = "Ежемесячная"
                        },
                        new
                        {
                            NewspaperId = 6,
                            Circulation = 6000,
                            EditorId = 6,
                            Name = "Наука сегодня",
                            Region = "Екатеринбург",
                            Type = "Ежемесячная"
                        },
                        new
                        {
                            NewspaperId = 7,
                            Circulation = 8000,
                            EditorId = 7,
                            Name = "Мир спорта",
                            Region = "Сочи",
                            Type = "Еженедельная"
                        });
                });

            modelBuilder.Entity("Editoria.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Advertisement", b =>
                {
                    b.HasOne("Editoria.Models.Entities.Issue", "Issue")
                        .WithMany("Advertisements")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Article", b =>
                {
                    b.HasOne("Editoria.Models.Entities.Author", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Editoria.Models.Entities.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Editoria.Models.Entities.Issue", "Issue")
                        .WithMany("Articles")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Issue", b =>
                {
                    b.HasOne("Editoria.Models.Entities.Newspaper", "Newspaper")
                        .WithMany("Issues")
                        .HasForeignKey("NewspaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Newspaper");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Newspaper", b =>
                {
                    b.HasOne("Editoria.Models.Entities.Editor", "Editor")
                        .WithOne("Newspaper")
                        .HasForeignKey("Editoria.Models.Entities.Newspaper", "EditorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editor");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Author", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Editor", b =>
                {
                    b.Navigation("Newspaper")
                        .IsRequired();
                });

            modelBuilder.Entity("Editoria.Models.Entities.Issue", b =>
                {
                    b.Navigation("Advertisements");

                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Editoria.Models.Entities.Newspaper", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
