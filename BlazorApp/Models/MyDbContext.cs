using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Klassen> Klassens { get; set; }

    public virtual DbSet<Lehrer> Lehrers { get; set; }

    public virtual DbSet<Schulzimmer> Schulzimmers { get; set; }

    public virtual DbSet<Stundenplan> Stundenplans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Prefer configuration via DI (see Program.cs). This fallback keeps the context usable
        // if it is instantiated without DI (e.g., quick scripts).
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StundenplanDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Klassen>(entity =>
        {
            entity.HasKey(e => e.KlassenId).HasName("PK__Klassen__5F7AE2F9D16D0ECD");

            entity.ToTable("Klassen");

            entity.HasIndex(e => e.KlassenName, "UQ__Klassen__ED1F50393A727DCB").IsUnique();

            entity.Property(e => e.KlassenId).HasColumnName("KlassenID");
            entity.Property(e => e.KlassenName).HasMaxLength(20);
        });

        modelBuilder.Entity<Lehrer>(entity =>
        {
            entity.HasKey(e => e.LehrerId).HasName("PK__Lehrer__08F2AB50C975B6B9");

            entity.ToTable("Lehrer");

            entity.Property(e => e.LehrerId).HasColumnName("LehrerID");
            entity.Property(e => e.LehrerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Schulzimmer>(entity =>
        {
            entity.HasKey(e => e.ZimmerId).HasName("PK__Schulzim__B86BAFFACAC8E291");

            entity.ToTable("Schulzimmer");

            entity.HasIndex(e => e.ZimmerBezeichnung, "UQ__Schulzim__5AD91645C3573CE2").IsUnique();

            entity.Property(e => e.ZimmerId).HasColumnName("ZimmerID");
            entity.Property(e => e.ZimmerBezeichnung).HasMaxLength(20);
        });

        modelBuilder.Entity<Stundenplan>(entity =>
        {
            entity.HasKey(e => e.StundenplanId).HasName("PK__Stundenp__121670BCD7C4CAF2");

            entity.ToTable("Stundenplan");

            entity.Property(e => e.StundenplanId).HasColumnName("StundenplanID");
            entity.Property(e => e.KlassenId).HasColumnName("KlassenID");
            entity.Property(e => e.LehrerId).HasColumnName("LehrerID");
            entity.Property(e => e.ZimmerId).HasColumnName("ZimmerID");

            entity.HasOne(d => d.Klassen).WithMany(p => p.Stundenplans)
                .HasForeignKey(d => d.KlassenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stundenplan_Klassen");

            entity.HasOne(d => d.Lehrer).WithMany(p => p.Stundenplans)
                .HasForeignKey(d => d.LehrerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stundenplan_Lehrer");

            entity.HasOne(d => d.Zimmer).WithMany(p => p.Stundenplans)
                .HasForeignKey(d => d.ZimmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stundenplan_Zimmer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
