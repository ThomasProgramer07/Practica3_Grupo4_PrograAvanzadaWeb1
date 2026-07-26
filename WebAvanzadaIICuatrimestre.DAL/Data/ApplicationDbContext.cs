using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.DAL.Entidades;

namespace WebAvanzadaIICuatrimestre.DAL.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PartidoPolitico> PartidoPoliticos { get; set; }

    public virtual DbSet<Voto> Votos { get; set; }

    public virtual DbSet<RepresentanteLegal> RepresentanteLegales { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    public virtual DbSet<Correo> Correos { get; set; }

    public virtual DbSet<Votante> Votantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // Intentionally left blank. Configure the provider externally.
        {
            if (!optionsBuilder.IsConfigured)
            {
                // No-op: connection configured via DI in application.
            }
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PartidoPolitico>(entity =>
        {
            entity.ToTable("PartidoPolitico");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Identificacion).HasDefaultValue("Sin Identificacion");
            entity.Property(e => e.Estado).HasColumnType("NUMERIC");
            entity.Property(e => e.Nombre).HasDefaultValue("Sin Nombre");
            entity.Property(e => e.Sigla).HasDefaultValue("Sin Sigla");
            entity.Property(e => e.FkrepresentanteLegal).HasColumnName("FKREPRESENTANTELEGAL");

            entity.HasOne(d => d.FkrepresentanteLegalNavigation).WithMany(p => p.PartidosPoliticos).HasForeignKey(d => d.FkrepresentanteLegal);
        });

        modelBuilder.Entity<Voto>(entity =>
        {
            entity.ToTable("Voto");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Fkvotante).HasColumnName("FKVOTANTE");
            entity.Property(e => e.FkpartidoPolitico).HasColumnName("FKPARTIDOPOLITICO");

            entity.HasOne(d => d.FkvotanteNavigation).WithMany().HasForeignKey(d => d.Fkvotante);
            entity.HasOne(d => d.FkpartidoPoliticoNavigation).WithMany().HasForeignKey(d => d.FkpartidoPolitico);
        });

        modelBuilder.Entity<RepresentanteLegal>(entity =>
        {
            entity.ToTable("RepresentanteLegal");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido2).HasDefaultValue("Sin apellido");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.ToTable("Telefono");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkrepresentanteLegal).HasColumnName("FKREPRESENTANTELEGAL");
            entity.Property(e => e.Fkvotante).HasColumnName("FKVOTANTE");
            
            entity.HasOne(d => d.FkrepresentanteLegalNavigation)
                .WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.FkrepresentanteLegal);

            entity.HasOne(d => d.FkvotanteNavigation)
                .WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.Fkvotante);

        });


        modelBuilder.Entity<Correo>(entity =>
        {
            entity.ToTable("Correo");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkrepresentanteLegal).HasColumnName("FKREPRESENTANTELEGAL");
            entity.Property(e => e.Fkvotante).HasColumnName("FKVOTANTE");
 
            entity.HasOne(d => d.FkrepresentanteLegalNavigation)
                .WithMany(p => p.Correos)
                .HasForeignKey(d => d.FkrepresentanteLegal);

            entity.HasOne(d => d.FkvotanteNavigation)
                .WithMany(p => p.Correos)
                .HasForeignKey(d => d.Fkvotante);
        });

        modelBuilder.Entity<Votante>(entity =>
        {
            entity.ToTable("Votante");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido2).HasDefaultValue("Sin apellido");
        });


        OnModelCreatingPartial(modelBuilder);
    }




    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
