﻿// <auto-generated />
using System;
using FakeNewsFilter.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FakeNewsFilter.Data.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("AppConfigs");

                    b.HasData(
                        new
                        {
                            Key = "Home Title",
                            Value = "This is homepage"
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Follow", b =>
                {
                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TopicId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Follow");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Language", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = "vi",
                            IsDefault = true,
                            Name = "Tiếng Việt"
                        },
                        new
                        {
                            Id = "en",
                            IsDefault = false,
                            Name = "English"
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Media", b =>
                {
                    b.Property<int>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("PathMedia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("MediaId");

                    b.ToTable("Media");

                    b.HasData(
                        new
                        {
                            MediaId = 1,
                            DateCreated = new DateTime(2021, 11, 18, 20, 40, 15, 45, DateTimeKind.Local).AddTicks(8710),
                            Duration = 0,
                            FileSize = 0L,
                            PathMedia = "covid.jpeg",
                            SortOrder = 0,
                            Type = 1
                        },
                        new
                        {
                            MediaId = 2,
                            DateCreated = new DateTime(2021, 11, 18, 20, 40, 15, 46, DateTimeKind.Local).AddTicks(430),
                            Duration = 0,
                            FileSize = 0L,
                            PathMedia = "taliban.jpeg",
                            SortOrder = 0,
                            Type = 1
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LanguageId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("OfficialRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int?>("ThumbNews")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("NewsId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ThumbNews")
                        .IsUnique()
                        .HasFilter("[ThumbNews] IS NOT NULL");

                    b.ToTable("News");

                    b.HasData(
                        new
                        {
                            NewsId = 1,
                            Content = "Test",
                            DatePublished = new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.",
                            LanguageId = "en",
                            Name = "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan",
                            Publisher = "New York Times",
                            Status = 0,
                            Timestamp = new DateTime(2021, 11, 18, 20, 40, 15, 46, DateTimeKind.Local).AddTicks(7080)
                        },
                        new
                        {
                            NewsId = 2,
                            Content = "Test",
                            DatePublished = new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.",
                            LanguageId = "en",
                            Name = "Texas high court blocks mask mandates in two of state's largest counties",
                            Publisher = "NBC News",
                            Status = 0,
                            Timestamp = new DateTime(2021, 11, 18, 20, 40, 15, 47, DateTimeKind.Local).AddTicks(20)
                        },
                        new
                        {
                            NewsId = 3,
                            Content = "Test",
                            DatePublished = new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..",
                            LanguageId = "en",
                            Name = "Hospitalizations of Americans under 50 have reached new pandemic highs",
                            Status = 0,
                            Timestamp = new DateTime(2021, 11, 18, 20, 40, 15, 47, DateTimeKind.Local).AddTicks(530)
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.NewsInTopics", b =>
                {
                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 11, 18, 20, 40, 14, 945, DateTimeKind.Local).AddTicks(1660));

                    b.HasKey("TopicId", "NewsId");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsInTopics");

                    b.HasData(
                        new
                        {
                            TopicId = 2,
                            NewsId = 3,
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 2,
                            NewsId = 2,
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 1,
                            NewsId = 1,
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a3314be5-4c77-4fb6-82ad-302014682a73"),
                            ConcurrencyStamp = "559c9658-ed7d-4eec-8047-4a5980054e6c",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("b4314be5-4c77-4fb6-82ad-302014682b13"),
                            ConcurrencyStamp = "a2e9cfd8-f9f6-495b-84e1-dc12ff153139",
                            Name = "Subscriber",
                            NormalizedName = "Subscriber"
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Source", b =>
                {
                    b.Property<int>("SourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LanguageId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("SourceName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("SourceId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Source");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Story", b =>
                {
                    b.Property<int>("StoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LanguageId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.Property<int?>("Thumbstory")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 11, 18, 20, 40, 14, 997, DateTimeKind.Local).AddTicks(5300));

                    b.HasKey("StoryId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("SourceId");

                    b.HasIndex("Thumbstory")
                        .IsUnique()
                        .HasFilter("[Thumbstory] IS NOT NULL");

                    b.ToTable("Story");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.TopicNews", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("normal");

                    b.Property<string>("LanguageId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("ThumbTopic")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("TopicId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ThumbTopic")
                        .IsUnique()
                        .HasFilter("[ThumbTopic] IS NOT NULL");

                    b.ToTable("TopicNews");

                    b.HasData(
                        new
                        {
                            TopicId = 1,
                            Description = "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.",
                            Label = "breaking",
                            LanguageId = "en",
                            Status = 0,
                            Tag = "afghanistan",
                            ThumbTopic = 2,
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 2,
                            Description = "Best nonfiction features, in-depth stores and other long-form content from across the web.",
                            Label = "featured",
                            LanguageId = "en",
                            Status = 0,
                            Tag = "in-depth",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 3,
                            Description = "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.",
                            Label = "featured",
                            LanguageId = "en",
                            Status = 0,
                            Tag = "coronavirus",
                            ThumbTopic = 1,
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 4,
                            Description = "The top business and economic news from around the world with a focus on the United State.",
                            Label = "featured",
                            LanguageId = "en",
                            Status = 0,
                            Tag = "top-business",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 5,
                            Description = "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.",
                            Label = "featured",
                            LanguageId = "en",
                            Status = 0,
                            Tag = "biden-admin",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 6,
                            Description = "Top stories from around the world with a focus on news not covered in other feeds.",
                            LanguageId = "en",
                            Status = 0,
                            Tag = "top-news",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 7,
                            Description = "Follow important local news: politics, business, top events and more. Updated everything evening.",
                            LanguageId = "en",
                            Status = 0,
                            Tag = "boston",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("AvatarId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId")
                        .IsUnique()
                        .HasFilter("[AvatarId] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "31960a04-15c4-46d9-9881-766049ab60f9",
                            Email = "bp.khuyen@hutech.edu.vn",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Bui Phu Khuyen",
                            NormalizedEmail = "BP.KHUYEN@HUTECH.EDU.VN",
                            NormalizedUserName = "khuyenpb",
                            PasswordHash = "AQAAAAEAACcQAAAAEBADc0zIS16mBakRt/IFZnpm5/aGaHekfH52Z8nKJSOleAnHCMpEjdLKoUa+h25WgQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = 0,
                            TwoFactorEnabled = false,
                            UserName = "khuyenpb"
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.UserRoles", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("a3314be5-4c77-4fb6-82ad-302014682a73")
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Vote", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 11, 18, 20, 40, 14, 991, DateTimeKind.Local).AddTicks(8570));

                    b.Property<bool>("isReal")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "NewsId");

                    b.HasIndex("NewsId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Follow", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.TopicNews", "TopicNews")
                        .WithMany("Follows")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.User", "User")
                        .WithMany("Follows")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TopicNews");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.News", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.Language", "Language")
                        .WithMany("News")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.Media", "Media")
                        .WithOne("News")
                        .HasForeignKey("FakeNewsFilter.Data.Entities.News", "ThumbNews");

                    b.Navigation("Language");

                    b.Navigation("Media");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.NewsInTopics", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.News", "News")
                        .WithMany("NewsInTopics")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.TopicNews", "TopicNews")
                        .WithMany("NewsInTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");

                    b.Navigation("TopicNews");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Source", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.Language", "Language")
                        .WithMany("Source")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Story", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.Language", "Language")
                        .WithMany("Story")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.Source", "Source")
                        .WithMany("Story")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.Media", "Media")
                        .WithOne("Story")
                        .HasForeignKey("FakeNewsFilter.Data.Entities.Story", "Thumbstory");

                    b.Navigation("Language");

                    b.Navigation("Media");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.TopicNews", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.Language", "Language")
                        .WithMany("TopicNews")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.Media", "Media")
                        .WithOne("TopicNews")
                        .HasForeignKey("FakeNewsFilter.Data.Entities.TopicNews", "ThumbTopic");

                    b.Navigation("Language");

                    b.Navigation("Media");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.User", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.Media", "Avatar")
                        .WithOne("User")
                        .HasForeignKey("FakeNewsFilter.Data.Entities.User", "AvatarId");

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.UserRoles", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Vote", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.News", "News")
                        .WithMany("Vote")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FakeNewsFilter.Data.Entities.User", "User")
                        .WithMany("Vote")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Language", b =>
                {
                    b.Navigation("News");

                    b.Navigation("Source");

                    b.Navigation("Story");

                    b.Navigation("TopicNews");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Media", b =>
                {
                    b.Navigation("News");

                    b.Navigation("Story");

                    b.Navigation("TopicNews");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.News", b =>
                {
                    b.Navigation("NewsInTopics");

                    b.Navigation("Vote");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Source", b =>
                {
                    b.Navigation("Story");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.TopicNews", b =>
                {
                    b.Navigation("Follows");

                    b.Navigation("NewsInTopics");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.User", b =>
                {
                    b.Navigation("Follows");

                    b.Navigation("UserRoles");

                    b.Navigation("Vote");
                });
#pragma warning restore 612, 618
        }
    }
}
