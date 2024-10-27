using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace web_app_csharp.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Shop;User Id=sa;Password=VeryStr0ngP@ssw0rd;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__014881AE317693A5");

            entity.Property(e => e.DeptId)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(4, 0)");
            entity.Property(e => e.Info)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.GoodId).HasName("PK__Goods__043AE53D59858FB4");

            entity.Property(e => e.DeptId).HasColumnType("decimal(4, 0)");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Producer)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Dept).WithMany(p => p.Goods)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK__Goods__DeptId__5165187F");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK__Sales__C952FB32454F58E0");

            entity.Property(e => e.DateSale).HasColumnType("date");

            entity.HasOne(d => d.Good).WithMany(p => p.Sales)
                .HasForeignKey(d => d.GoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__GoodId__5441852A");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkersId).HasName("PK__Workers__F1CA494C02533835");

            entity.Property(e => e.Address)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.DeptId).HasColumnType("decimal(4, 0)");
            entity.Property(e => e.Information).HasMaxLength(20);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Dept).WithMany(p => p.Workers)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK__Workers__DeptId__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
