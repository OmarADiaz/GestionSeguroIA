using CreditRiskAgent.Application.Common;
using CreditRiskAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CreditRiskAgent.Infrastructure.Persistence
{
    public class SegurosDbContext : DbContext, IApplicationDbContext
    {
        public SegurosDbContext(DbContextOptions<SegurosDbContext> options) : base(options) { }

        public DbSet<Asegurado> Asegurados => Set<Asegurado>();
        public DbSet<Ramo> Ramos => Set<Ramo>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Poliza> Polizas => Set<Poliza>();
        public DbSet<CoberturaPoliza> CoberturasPoliza => Set<CoberturaPoliza>();
        public DbSet<Siniestro> Siniestros => Set<Siniestro>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Póliza - Asegurado
            modelBuilder.Entity<Poliza>()
                .HasOne(p => p.Asegurado)
                .WithMany(a => a.Polizas)
                .HasForeignKey(p => p.IdAsegurado);

            // Relación Póliza - Producto
            modelBuilder.Entity<Poliza>()
                .HasOne(p => p.Producto)
                .WithMany(p => p.Polizas)
                .HasForeignKey(p => p.IdProducto);

            // Relación Producto - Ramo
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Ramo)
                .WithMany(r => r.Productos)
                .HasForeignKey(p => p.IdRamo);

            // Relación Póliza - Siniestros
            modelBuilder.Entity<Siniestro>()
                .HasOne(s => s.Poliza)
                .WithMany(p => p.Siniestros)
                .HasForeignKey(s => s.IdPoliza);

            // Relación Póliza - Coberturas
            modelBuilder.Entity<CoberturaPoliza>()
                .HasOne(c => c.Poliza)
                .WithMany(p => p.Coberturas)
                .HasForeignKey(c => c.IdPoliza);
        }
    }
}
