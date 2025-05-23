﻿using Microsoft.EntityFrameworkCore;
using PTP.EntityLayer.Models;

namespace PTP.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProcessStage> ProcessStages { get; set; }
        public DbSet<ProjectPersonnel> ProjectPersonnels { get; set; }
        public DbSet<ProcessPersonnel> ProcessPersonnels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.Project)
                .WithMany(pj => pj.Processes)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Process)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ProcessId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Process)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Personnel)
                .WithMany()
                .HasForeignKey(c => c.PersonnelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Personnel)
                .WithMany()
                .HasForeignKey(d => d.PersonnelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Project)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Personnel)
                .WithOne(p => p.User)
                .HasForeignKey<Personnel>(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.UserId);

            modelBuilder.Entity<ProjectPersonnel>()
                .HasKey(pp => pp.Id); 

            modelBuilder.Entity<ProjectPersonnel>()
                .HasIndex(pp => new { pp.ProjectId, pp.PersonnelId }).IsUnique();

            modelBuilder.Entity<ProcessPersonnel>()
                .HasOne(pp => pp.Process)
                .WithMany(p => p.ProcessPersonnels)
                .HasForeignKey(pp => pp.ProcessId);

            modelBuilder.Entity<ProcessPersonnel>()
                .HasOne(pp => pp.Personnel)
                .WithMany(p => p.ProcessPersonnels)
                .HasForeignKey(pp => pp.PersonnelId);



        }
    }
}
