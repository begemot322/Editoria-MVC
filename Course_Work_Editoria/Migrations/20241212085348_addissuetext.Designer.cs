﻿// <auto-generated />
using System;
using Course_Work_Editoria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Course_Work_Editoria.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241212085348_addissuetext")]
    partial class addissuetext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Course_Work_Editoria.Models.Advertisement", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdvertisementId");

                    b.HasIndex("IssueId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Article", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("IssueId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Author", b =>
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

            modelBuilder.Entity("Course_Work_Editoria.Models.Category", b =>
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

            modelBuilder.Entity("Course_Work_Editoria.Models.Editor", b =>
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

            modelBuilder.Entity("Course_Work_Editoria.Models.Issue", b =>
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
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Newspaper", b =>
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
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Advertisement", b =>
                {
                    b.HasOne("Course_Work_Editoria.Models.Issue", "Issue")
                        .WithMany("Advertisements")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Article", b =>
                {
                    b.HasOne("Course_Work_Editoria.Models.Author", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Course_Work_Editoria.Models.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Course_Work_Editoria.Models.Issue", "Issue")
                        .WithMany("Articles")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Issue", b =>
                {
                    b.HasOne("Course_Work_Editoria.Models.Newspaper", "Newspaper")
                        .WithMany("Issues")
                        .HasForeignKey("NewspaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Newspaper");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Newspaper", b =>
                {
                    b.HasOne("Course_Work_Editoria.Models.Editor", "Editor")
                        .WithOne("Newspaper")
                        .HasForeignKey("Course_Work_Editoria.Models.Newspaper", "EditorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editor");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Author", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Editor", b =>
                {
                    b.Navigation("Newspaper")
                        .IsRequired();
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Issue", b =>
                {
                    b.Navigation("Advertisements");

                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Course_Work_Editoria.Models.Newspaper", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
