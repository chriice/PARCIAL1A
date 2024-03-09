using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models;

public partial class Parcial1aContext : DbContext
{
    public Parcial1aContext()
    {
    }

    public Parcial1aContext(DbContextOptions<Parcial1aContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Autorlibro> Autorlibros { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-H6VGDNU2; Database=PARCIAL1A; Trusted_Connection=True; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__autores__3213E83FFF6544B9");

            entity.ToTable("autores");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Autorlibro>(entity =>
        {
            entity.HasKey(e => new { e.Autorid, e.Libroid }).HasName("PK__autorlib__81EFF31575396523");

            entity.ToTable("autorlibro");

            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Libroid).HasColumnName("libroid");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.Autor).WithMany(p => p.Autorlibros)
                .HasForeignKey(d => d.Autorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__autorlibr__autor__440B1D61");

            entity.HasOne(d => d.Libro).WithMany(p => p.Autorlibros)
                .HasForeignKey(d => d.Libroid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__autorlibr__libro__44FF419A");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__libros__3213E83FE3A3AFC6");

            entity.ToTable("libros");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__posts__3213E83F0FEE808F");

            entity.ToTable("posts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Contenido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contenido");
            entity.Property(e => e.Fechapublicacion)
                .HasColumnType("datetime")
                .HasColumnName("fechapublicacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Autor).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Autorid)
                .HasConstraintName("FK__posts__autorid__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
