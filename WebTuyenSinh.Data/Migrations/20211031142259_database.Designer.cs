﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTuyenSinh.Data.Entityes;

namespace WebTuyenSinh.Data.Migrations
{
    [DbContext(typeof(HeThongTuyenSinhDB))]
    [Migration("20211031142259_database")]
    partial class database
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BrithDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirtName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

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

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nchar(11)")
                        .IsFixedLength(true)
                        .HasMaxLength(11);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Addmisstion_Major_Block", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("idAdmisstion")
                        .HasColumnType("bigint");

                    b.Property<string>("idBlock")
                        .HasColumnType("nchar(9)")
                        .IsFixedLength(true)
                        .HasMaxLength(9);

                    b.HasKey("id");

                    b.HasIndex("idAdmisstion");

                    b.HasIndex("idBlock");

                    b.ToTable("Addmisstion_Major_Block");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Admisstion", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("OpenTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("Statust")
                        .HasColumnType("bit");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Admisstion");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Admisstion_Major", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OpenTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("Statust")
                        .HasColumnType("bit");

                    b.Property<long?>("idAdmisstion")
                        .HasColumnType("bigint");

                    b.Property<string>("idMajor")
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true)
                        .HasMaxLength(10);

                    b.HasKey("id");

                    b.HasIndex("idAdmisstion");

                    b.HasIndex("idMajor");

                    b.ToTable("Admisstion_Major");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Advise", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Telephone")
                        .HasColumnType("nchar(11)")
                        .IsFixedLength(true)
                        .HasMaxLength(11);

                    b.HasKey("id");

                    b.ToTable("Advise");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRole");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Block", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nchar(9)")
                        .IsFixedLength(true)
                        .HasMaxLength(9);

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Desscription")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.ToTable("Block");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Catergory", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Delete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.ToTable("Catergory");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.CatergoryDetail", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Detete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("Statust")
                        .HasColumnType("bit");

                    b.Property<long?>("idCatergory")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("idCatergory");

                    b.ToTable("CatergoryDetails");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.InforMationProflie", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Statust")
                        .HasColumnType("bit");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.Property<string>("idBlock")
                        .HasColumnType("nchar(9)")
                        .IsFixedLength(true)
                        .HasMaxLength(9);

                    b.Property<DateTime?>("idMajor")
                        .HasColumnType("datetime2");

                    b.Property<long?>("idProfile")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("idBlock");

                    b.HasIndex("idProfile");

                    b.ToTable("InforMationProflie");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Major", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true)
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.Property<bool?>("delete")
                        .HasColumnType("bit");

                    b.Property<string>("imgMajor")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("id");

                    b.ToTable("Major");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Post", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypePost")
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true)
                        .HasMaxLength(10);

                    b.Property<long?>("idCategory")
                        .HasColumnType("bigint");

                    b.Property<int?>("view")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("idCategory");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.ProfileStudent", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("AdressRange")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("date");

                    b.Property<string>("CMND")
                        .HasColumnType("nchar(12)")
                        .IsFixedLength(true)
                        .HasMaxLength(12);

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRange")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("FromBirthDay")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Nation")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("OpenTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Sex")
                        .HasColumnType("int");

                    b.Property<long?>("Shoo1")
                        .HasColumnType("bigint");

                    b.Property<long?>("Shoo2")
                        .HasColumnType("bigint");

                    b.Property<long?>("Shoo3")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Statust")
                        .HasColumnType("bit");

                    b.Property<string>("Teletephone")
                        .HasColumnType("char(11)")
                        .IsFixedLength(true)
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updatedate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.Property<string>("idAccount")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("idAdmisstion")
                        .HasColumnType("bigint");

                    b.Property<string>("imgavata")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("url")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("id");

                    b.HasIndex("idAccount");

                    b.HasIndex("idAdmisstion");

                    b.ToTable("ProfileStudent");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.School", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adrees")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("NameConscious")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("NameDistrict")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("NameShool")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("idConscious")
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<string>("idDistrict")
                        .HasColumnType("nchar(12)")
                        .IsFixedLength(true)
                        .HasMaxLength(12);

                    b.Property<string>("idShool")
                        .HasColumnType("nchar(12)")
                        .IsFixedLength(true)
                        .HasMaxLength(12);

                    b.HasKey("id");

                    b.ToTable("School");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Silde", b =>
                {
                    b.Property<long>("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2")
                        .IsFixedLength(true);

                    b.Property<DateTime?>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .IsFixedLength(true)
                        .HasDefaultValue(new DateTime(2021, 10, 31, 21, 22, 59, 151, DateTimeKind.Local).AddTicks(6848));

                    b.Property<DateTime?>("OpenTime")
                        .HasColumnType("datetime2")
                        .IsFixedLength(true);

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .IsFixedLength(true);

                    b.Property<bool?>("delete")
                        .HasColumnType("bit")
                        .IsFixedLength(true);

                    b.HasKey("id");

                    b.ToTable("Silde");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.file", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<long?>("idPost")
                        .HasColumnType("bigint");

                    b.Property<string>("url")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.HasIndex("idPost");

                    b.ToTable("files");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Addmisstion_Major_Block", b =>
                {
                    b.HasOne("WebTuyenSinh.Data.Entityes.Admisstion_Major", "Admisstion_Major")
                        .WithMany("Addmisstion_Major_Block")
                        .HasForeignKey("idAdmisstion");

                    b.HasOne("WebTuyenSinh.Data.Entityes.Block", "Block")
                        .WithMany("Addmisstion_Major_Block")
                        .HasForeignKey("idBlock");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Admisstion_Major", b =>
                {
                    b.HasOne("WebTuyenSinh.Data.Entityes.Admisstion", "Admisstion")
                        .WithMany("Admisstion_Major")
                        .HasForeignKey("idAdmisstion");

                    b.HasOne("WebTuyenSinh.Data.Entityes.Major", "Major")
                        .WithMany("Admisstion_Major")
                        .HasForeignKey("idMajor");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.CatergoryDetail", b =>
                {
                    b.HasOne("WebTuyenSinh.Data.Entityes.Catergory", "Catergory")
                        .WithMany("CatergoryDetails")
                        .HasForeignKey("idCatergory");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.InforMationProflie", b =>
                {
                    b.HasOne("WebTuyenSinh.Data.Entityes.Block", "Block")
                        .WithMany("InforMationProflies")
                        .HasForeignKey("idBlock");

                    b.HasOne("WebTuyenSinh.Data.Entityes.ProfileStudent", "ProfileStudent")
                        .WithMany("InforMationProflies")
                        .HasForeignKey("idProfile");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.Post", b =>
                {
                    b.HasOne("WebTuyenSinh.Data.Entityes.CatergoryDetail", "CatergoryDetail")
                        .WithMany("Posts")
                        .HasForeignKey("idCategory");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.ProfileStudent", b =>
                {
                    b.HasOne("WebTuyenSinh.Data.Entityes.Account", "Account")
                        .WithMany("ProfileStudents")
                        .HasForeignKey("idAccount");

                    b.HasOne("WebTuyenSinh.Data.Entityes.Admisstion", "Admisstion")
                        .WithMany("ProfileStudents")
                        .HasForeignKey("idAdmisstion");
                });

            modelBuilder.Entity("WebTuyenSinh.Data.Entityes.file", b =>
                {
                    b.HasOne("WebTuyenSinh.Data.Entityes.Post", "Post")
                        .WithMany("files")
                        .HasForeignKey("idPost");
                });
#pragma warning restore 612, 618
        }
    }
}
