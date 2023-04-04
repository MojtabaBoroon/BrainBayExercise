﻿// <auto-generated />
using System;
using BrainbayConsoleApp.DataAccessContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrainbayConsoleApp.Migrations
{
    [DbContext(typeof(BrainBayDbContext))]
    [Migration("20230403122903_make some properties nullable")]
    partial class makesomepropertiesnullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BrainbayConsoleApp.Entites.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OriginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Species")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OriginId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("BrainbayConsoleApp.Entites.Episode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("BrainbayConsoleApp.Entites.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("BrainbayConsoleApp.Entites.Origin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Origins");
                });

            modelBuilder.Entity("BrainbayConsoleApp.Entites.Character", b =>
                {
                    b.HasOne("BrainbayConsoleApp.Entites.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("BrainbayConsoleApp.Entites.Origin", "Origin")
                        .WithMany()
                        .HasForeignKey("OriginId");

                    b.Navigation("Location");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("BrainbayConsoleApp.Entites.Episode", b =>
                {
                    b.HasOne("BrainbayConsoleApp.Entites.Character", null)
                        .WithMany("Episodes")
                        .HasForeignKey("CharacterId");
                });

            modelBuilder.Entity("BrainbayConsoleApp.Entites.Character", b =>
                {
                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
