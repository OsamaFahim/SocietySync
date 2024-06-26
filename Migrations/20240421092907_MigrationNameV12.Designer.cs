﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocietySync.DBcontext;

#nullable disable

namespace SocietySync.Migrations
{
    [DbContext(typeof(SocietySyncContext))]
    [Migration("20240421092907_MigrationNameV12")]
    partial class MigrationNameV12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Society", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Goals")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresidentRollNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Name");

                    b.HasIndex("PresidentRollNum");

                    b.ToTable("Societies");
                });

            modelBuilder.Entity("SocietySync.Models.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostedByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostedByUserId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("SocietySync.Models.Event", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Society_Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocietyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name", "Society_Name")
                        .HasName("Event_ID");

                    b.HasIndex("SocietyName");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("SocietySync.Models.SocietyMembership", b =>
                {
                    b.Property<string>("Member_RollNum")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Society_Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocietyName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserRollNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Member_RollNum", "Society_Name")
                        .HasName("ID");

                    b.HasIndex("SocietyName");

                    b.HasIndex("UserRollNum");

                    b.ToTable("SocietyMemberships");
                });

            modelBuilder.Entity("SocietySync.Models.User", b =>
                {
                    b.Property<string>("RollNum")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Hashed_Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RollNum");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Society", b =>
                {
                    b.HasOne("SocietySync.Models.User", "President")
                        .WithMany("PresidentSocieties")
                        .HasForeignKey("PresidentRollNum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("President");
                });

            modelBuilder.Entity("SocietySync.Models.Announcement", b =>
                {
                    b.HasOne("SocietySync.Models.User", "PostedByUser")
                        .WithMany("Announcements")
                        .HasForeignKey("PostedByUserId");

                    b.Navigation("PostedByUser");
                });

            modelBuilder.Entity("SocietySync.Models.Event", b =>
                {
                    b.HasOne("Society", "Society")
                        .WithMany("Events")
                        .HasForeignKey("SocietyName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Society");
                });

            modelBuilder.Entity("SocietySync.Models.SocietyMembership", b =>
                {
                    b.HasOne("Society", "Society")
                        .WithMany("Memberships")
                        .HasForeignKey("SocietyName");

                    b.HasOne("SocietySync.Models.User", "User")
                        .WithMany("SocietyMemberships")
                        .HasForeignKey("UserRollNum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Society");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Society", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Memberships");
                });

            modelBuilder.Entity("SocietySync.Models.User", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("PresidentSocieties");

                    b.Navigation("SocietyMemberships");
                });
#pragma warning restore 612, 618
        }
    }
}
