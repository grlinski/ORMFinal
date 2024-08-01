﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ORMFinal.DAL;

#nullable disable

namespace ORMFinal.DAL.Migrations
{
    [DbContext(typeof(ORMFinalContext))]
    [Migration("20240801233905_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeExhibit", b =>
                {
                    b.Property<int>("EmployeesEmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ExhibitsExhibitId")
                        .HasColumnType("int");

                    b.HasKey("EmployeesEmployeeId", "ExhibitsExhibitId");

                    b.HasIndex("ExhibitsExhibitId");

                    b.ToTable("ExhibitEmployees", (string)null);
                });

            modelBuilder.Entity("ORMFinal.Models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimalId"));

                    b.Property<string>("AnimalCategory")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Habitat")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AnimalId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("ORMFinal.Models.AnimalHealth", b =>
                {
                    b.Property<int>("HealthReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HealthReportId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("HealthStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("LastVaccinationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.HasKey("HealthReportId");

                    b.HasIndex("AnimalId")
                        .IsUnique();

                    b.ToTable("AnimalHealths");
                });

            modelBuilder.Entity("ORMFinal.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<DateTime?>("DateEnded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStarted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ORMFinal.Models.Exhibit", b =>
                {
                    b.Property<int>("ExhibitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExhibitId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExhibitId");

                    b.HasIndex("AnimalId");

                    b.ToTable("Exhibits");
                });

            modelBuilder.Entity("ORMFinal.Models.FeedingSchedule", b =>
                {
                    b.Property<int>("FeedingScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedingScheduleId"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EveningFeeding")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MorningFeeding")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NightFeeding")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NoonFeeding")
                        .HasColumnType("datetime2");

                    b.HasKey("FeedingScheduleId");

                    b.HasIndex("AnimalId")
                        .IsUnique();

                    b.ToTable("FeedingSchedules");
                });

            modelBuilder.Entity("EmployeeExhibit", b =>
                {
                    b.HasOne("ORMFinal.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ORMFinal.Models.Exhibit", null)
                        .WithMany()
                        .HasForeignKey("ExhibitsExhibitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ORMFinal.Models.AnimalHealth", b =>
                {
                    b.HasOne("ORMFinal.Models.Animal", "Animal")
                        .WithOne("AnimalHealth")
                        .HasForeignKey("ORMFinal.Models.AnimalHealth", "AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("ORMFinal.Models.Exhibit", b =>
                {
                    b.HasOne("ORMFinal.Models.Animal", "Animal")
                        .WithMany("Exhibits")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("ORMFinal.Models.FeedingSchedule", b =>
                {
                    b.HasOne("ORMFinal.Models.Animal", "Animal")
                        .WithOne("FeedingSchedule")
                        .HasForeignKey("ORMFinal.Models.FeedingSchedule", "AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("ORMFinal.Models.Animal", b =>
                {
                    b.Navigation("AnimalHealth")
                        .IsRequired();

                    b.Navigation("Exhibits");

                    b.Navigation("FeedingSchedule")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
