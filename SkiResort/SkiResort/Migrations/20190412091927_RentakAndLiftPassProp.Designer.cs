﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkiResort.Data;

namespace SkiResort.Migrations
{
    [DbContext(typeof(SkiResortContext))]
    [Migration("20190412091927_RentakAndLiftPassProp")]
    partial class RentakAndLiftPassProp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SkiResort.Data.Models.Hike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AverageDuration");

                    b.Property<string>("EndPoint")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("StartPoint")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Hikes");
                });

            modelBuilder.Entity("SkiResort.Data.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<decimal>("Price");

                    b.Property<int?>("RentalId");

                    b.Property<string>("Size")
                        .IsRequired();

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("RentalId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SkiResort.Data.Models.Lift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Length");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("NightSkiing");

                    b.Property<decimal>("VerticalRise");

                    b.Property<string>("WorkingHours")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Lifts");
                });

            modelBuilder.Entity("SkiResort.Data.Models.LiftPass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<double>("Duration");

                    b.Property<double>("Price");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("liftPasses");
                });

            modelBuilder.Entity("SkiResort.Data.Models.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HikeId");

                    b.Property<int>("Stars")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.HasIndex("HikeId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("SkiResort.Data.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("SkiResort.Data.Models.Trail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LiftId");

                    b.Property<string>("Mode")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Trails");
                });

            modelBuilder.Entity("SkiResort.Data.Models.Item", b =>
                {
                    b.HasOne("SkiResort.Data.Models.Rental", "Rental")
                        .WithMany("Items")
                        .HasForeignKey("RentalId");
                });

            modelBuilder.Entity("SkiResort.Data.Models.Rate", b =>
                {
                    b.HasOne("SkiResort.Data.Models.Hike", "Hike")
                        .WithMany("Rates")
                        .HasForeignKey("HikeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}