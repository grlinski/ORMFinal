using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ORMFinal.Models;

namespace ORMFinal.DAL
{
    public class ORMFinalContext : DbContext
    {
        public ORMFinalContext(DbContextOptions<ORMFinalContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Exhibit> Exhibits { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FeedingSchedule> FeedingSchedules { get; set; }
        public DbSet<AnimalHealth> AnimalHealths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Relations
            //Animals to FeedingSchedule 1 to 1
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.FeedingSchedule)
                .WithOne(f => f.Animal)
                .HasForeignKey<FeedingSchedule>(f => f.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            //AnimalHealth to Animals 1 to 1
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.AnimalHealth)
                .WithOne(h => h.Animal)
                .HasForeignKey<AnimalHealth>(h => h.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Animals to Exhibit 1 to many
            modelBuilder.Entity<Exhibit>()
                .HasOne(e => e.Animal)
                .WithMany(a => a.Exhibits)
                .HasForeignKey(e => e.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Exhibit)
                .WithMany()
                .HasForeignKey(e => e.ExhibitId)
                .OnDelete(DeleteBehavior.Cascade);


            //Contraints
            modelBuilder.Entity<Animal>()
                .Property(a => a.AnimalCategory)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Animal>()
                .Property(a => a.Habitat)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Exhibit>()
                .Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Exhibit>()
                .Property(e => e.Size)
                .IsRequired();


            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<AnimalHealth>()
                .Property(h => h.HealthStatus)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}