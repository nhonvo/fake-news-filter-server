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

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MediaId");

                    b.ToTable("Media");

                    b.HasData(
                        new
                        {
                            MediaId = 1,
                            DateCreated = new DateTime(2021, 8, 19, 21, 9, 1, 418, DateTimeKind.Local).AddTicks(1750),
                            Duration = 0,
                            FileSize = 0L,
                            SortOrder = 0,
                            Type = 1,
                            Url = "https://static01.nyt.com/images/2021/08/15/world/15afghanistan-kabul-airport/merlin_193320777_09900a3b-bd82-47c6-ad73-fddc1219018d-superJumbo.jpg?quality=90&auto=webp"
                        },
                        new
                        {
                            MediaId = 2,
                            DateCreated = new DateTime(2021, 8, 19, 21, 9, 1, 435, DateTimeKind.Local).AddTicks(8750),
                            Duration = 0,
                            FileSize = 0L,
                            SortOrder = 0,
                            Type = 1,
                            Url = "https://media-cldnry.s-nbcnews.com/image/upload/t_fit-2000w,f_auto,q_auto:best/newscms/2021_30/3495573/210730-greg-abbott-ew-617p.jpg"
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("MediaNews")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("OfficialRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SocialBeliefs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<string>("SourceLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("NewsId");

                    b.HasIndex("MediaNews")
                        .IsUnique()
                        .HasFilter("[MediaNews] IS NOT NULL");

                    b.ToTable("News");

                    b.HasData(
                        new
                        {
                            NewsId = 1,
                            Description = "Taliban fighters poured into the Afghan capital on Sunday amid scenes of panic and chaos, bringing a swift and shocking close to the Afghan government and the 20-year American era in the country.",
                            MediaNews = 1,
                            Name = "Kabul’s Sudden Fall to Taliban Ends U.S. Era in Afghanistan",
                            SocialBeliefs = 0.0,
                            SourceLink = "https://www.nytimes.com/2021/08/15/world/asia/afghanistan-taliban-kabul-surrender.html",
                            Timestamp = new DateTime(2021, 8, 19, 21, 9, 1, 436, DateTimeKind.Local).AddTicks(3570)
                        },
                        new
                        {
                            NewsId = 2,
                            Description = "The masking orders in Dallas and Bexar counties were issued after a lower court ruled last week in favor of local officials.",
                            MediaNews = 2,
                            Name = "Texas high court blocks mask mandates in two of state's largest counties",
                            SocialBeliefs = 0.0,
                            SourceLink = "https://www.nbcnews.com/news/us-news/texas-high-court-blocks-mask-mandates-two-state-s-largest-n1276884",
                            Timestamp = new DateTime(2021, 8, 19, 21, 9, 1, 436, DateTimeKind.Local).AddTicks(5720)
                        },
                        new
                        {
                            NewsId = 3,
                            Description = "A lagging vaccination campaign and the spread of the highly contagious Delta variant are driving a surge in Covid-19 hospitalizations in the United States..",
                            Name = "Hospitalizations of Americans under 50 have reached new pandemic highs",
                            SocialBeliefs = 0.0,
                            SourceLink = "https://www.nytimes.com/live/2021/08/15/world/covid-delta-variant-vaccine/covid-hospitalizations-cdc",
                            Timestamp = new DateTime(2021, 8, 19, 21, 9, 1, 436, DateTimeKind.Local).AddTicks(6130)
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.NewsInTopics", b =>
                {
                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.HasKey("TopicId", "NewsId");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsInTopics");

                    b.HasData(
                        new
                        {
                            TopicId = 2,
                            NewsId = 3
                        },
                        new
                        {
                            TopicId = 2,
                            NewsId = 2
                        },
                        new
                        {
                            TopicId = 1,
                            NewsId = 1
                        });
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

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
                            ConcurrencyStamp = "1cba88d1-eb30-4e8e-9d7d-6a7792f40a8f",
                            Description = "System Admin",
                            Name = "admin",
                            NormalizedName = "admin"
                        });
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MediaTopic")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("TopicId");

                    b.HasIndex("MediaTopic")
                        .IsUnique()
                        .HasFilter("[MediaTopic] IS NOT NULL");

                    b.ToTable("TopicNews");

                    b.HasData(
                        new
                        {
                            TopicId = 1,
                            Description = "Follow live as the Taliban seizes territory across Afghanistan in the wake of the U.S. withdrawal.",
                            Label = "breaking",
                            Tag = "afghanistan",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 2,
                            Description = "Best nonfiction features, in-depth stores and other long-form content from across the web.",
                            Label = "featured",
                            Tag = "in-depth",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 3,
                            Description = "Outbreak of respiratory virus that has killed over 1 million and infected 100 milion worldwide.",
                            Label = "feature",
                            Tag = "coronavirus",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 4,
                            Description = "The top business and economic news from around the world with a focus on the United State.",
                            Label = "feature",
                            Tag = "top-business",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 5,
                            Description = "Follow the presidential transition of Joe Biden, including policy plans, appointments and more.",
                            Label = "feature",
                            Tag = "biden-admin",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 6,
                            Description = "Top stories from around the world with a focus on news not covered in other feeds.",
                            Tag = "top-news",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TopicId = 7,
                            Description = "Follow important local news: politics, business, top events and more. Updated everything evening.",
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

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2de4b27f-d6fe-4525-a797-99fb472cf83e",
                            Email = "bp.khuyen@hutech.edu.vn",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Bui Phu Khuyen",
                            NormalizedEmail = "BP.KHUYEN@HUTECH.EDU.VN",
                            NormalizedUserName = "khuyenpb",
                            PasswordHash = "AQAAAAEAACcQAAAAEJONeBe9AXkK5C5wcR/5QpmRwmizSzcb5ysyzxQwV7WdubFhXZak/uRS7fEd9Q5LuA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = 0,
                            TwoFactorEnabled = false,
                            UserName = "khuyenpb"
                        });
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69db714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("a3314be5-4c77-4fb6-82ad-302014682a73")
                        });
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
                    b.HasOne("FakeNewsFilter.Data.Entities.Media", "Media")
                        .WithOne("News")
                        .HasForeignKey("FakeNewsFilter.Data.Entities.News", "MediaNews");

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

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.TopicNews", b =>
                {
                    b.HasOne("FakeNewsFilter.Data.Entities.Media", "Media")
                        .WithOne("TopicNews")
                        .HasForeignKey("FakeNewsFilter.Data.Entities.TopicNews", "MediaTopic");

                    b.Navigation("Media");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.Media", b =>
                {
                    b.Navigation("News");

                    b.Navigation("TopicNews");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.News", b =>
                {
                    b.Navigation("NewsInTopics");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.TopicNews", b =>
                {
                    b.Navigation("Follows");

                    b.Navigation("NewsInTopics");
                });

            modelBuilder.Entity("FakeNewsFilter.Data.Entities.User", b =>
                {
                    b.Navigation("Follows");
                });
#pragma warning restore 612, 618
        }
    }
}
