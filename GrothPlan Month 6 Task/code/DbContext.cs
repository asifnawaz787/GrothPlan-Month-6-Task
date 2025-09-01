using GrothPlan_Month_6_Task.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GrothPlan_Month_6_Task.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Employee table
            builder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                
            });
            builder.Entity<Employee>()
                .HasMany(e => e.Roles)
                .WithOne(r => r.Employee)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            // Document table
            builder.Entity<Document>(entity =>
            {
                entity.ToTable("Documents");
                entity.HasKey(d => d.Id);

                entity.Property(d => d.FileName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasOne(d => d.Employee)
                      .WithMany(emp => emp.Document) // plural
                      .HasForeignKey(d => d.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Employee>()
                .HasMany(e => e.Document)
                .WithOne(r => r.Employee)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            // Role table
            builder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(r => r.RoleId);

                entity.Property(r => r.RoleName).IsRequired().HasMaxLength(50);

                entity.HasOne(r => r.Employee)
                      .WithMany(emp => emp.Roles)
                      .HasForeignKey(r => r.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
