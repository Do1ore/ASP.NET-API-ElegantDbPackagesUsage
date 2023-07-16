﻿// <auto-generated />
using System;
using Infrastructure.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(EfCorePhotosContext))]
    partial class EfCorePhotosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.5.23280.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("AbsolutePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("AbsolutePath");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FileExtension");

                    b.Property<string>("PhotoName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PhotoName");

                    b.Property<Guid?>("PhotographerId")
                        .HasColumnType("uuid")
                        .HasColumnName("PhotographerId");

                    b.HasKey("Id");

                    b.HasIndex("PhotographerId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Domain.Entities.Photographer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("WasBorn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Photographers");
                });

            modelBuilder.Entity("Domain.Entities.Photo", b =>
                {
                    b.HasOne("Domain.Entities.Photographer", "Photographer")
                        .WithMany()
                        .HasForeignKey("PhotographerId");

                    b.Navigation("Photographer");
                });
#pragma warning restore 612, 618
        }
    }
}
