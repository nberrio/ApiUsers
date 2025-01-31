using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInterminedio.Models;

namespace PruebaTecnicaInterminedio.Data;

public partial class PruebaJuniorContext : DbContext
{
    public PruebaJuniorContext()
    {
    }

    public PruebaJuniorContext(DbContextOptions<PruebaJuniorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NicolasB;Database=PruebaJunior;User ID=sa;password=942619;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity
                .HasKey(u => u.Id);
                entity.ToTable("Usuario");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.BurnsDay)
                .HasColumnType("datetime")
                .HasColumnName("burnsDay");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.GuidUser)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("guidUser");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.NameUser)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nameUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
