﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocietySync.DBcontext;

#nullable disable

namespace SocietySync.Migrations
{
    [DbContext(typeof(SocietySyncContext))]
    partial class SocietySyncContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
#pragma warning restore 612, 618
        }
    }
}
