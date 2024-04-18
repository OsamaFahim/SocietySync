﻿// <auto-generated />
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
    [Migration("20240417213022_MigrationNameV5")]
    partial class MigrationNameV5
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
                        .WithMany("PresidedSocieties")
                        .HasForeignKey("PresidentRollNum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("President");
                });

            modelBuilder.Entity("SocietySync.Models.User", b =>
                {
                    b.Navigation("PresidedSocieties");
                });
#pragma warning restore 612, 618
        }
    }
}