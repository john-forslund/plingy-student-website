﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plingy.Models;

namespace Plingy.Migrations
{
    [DbContext(typeof(PlingyDbContext))]
    [Migration("20200416084525_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Plingy.Models.Allergies", b =>
                {
                    b.Property<int>("AllergiesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Allergy")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AllergiesID");

                    b.HasIndex("StudentID");

                    b.ToTable("Allergies");
                });

            modelBuilder.Entity("Plingy.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ActiveStudent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("StudentID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Plingy.Models.Allergies", b =>
                {
                    b.HasOne("Plingy.Models.Student", "Student")
                        .WithMany("Allergies")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
