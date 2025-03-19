using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem1.Models;

public partial class RestaurantDbContext : DbContext
{
    public RestaurantDbContext()
    {
    }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-NMSPM5R4\\SQLEXPRESS02;Database=RestaurantDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0758868DBD");

            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuItem__3214EC0793F601E0");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC077AF52CF1");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053454706DE3").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
