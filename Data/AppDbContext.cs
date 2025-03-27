using Microsoft.EntityFrameworkCore;
using Desafio.Models;

namespace Desafio.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Productos> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>(x =>
            {
                x.HasKey(e => e.IdCliente);

                x.Property(e => e.IdCliente).
                UseIdentityColumn()
                .ValueGeneratedOnAdd();
                x.ToTable("CLIENTE");
                x.Property(e => e.Apellido).HasMaxLength(50);
                x.Property(e => e.Direccion).HasMaxLength(50);
                x.Property(e => e.Nombre).HasMaxLength(50);
                x.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Venta>(x =>
            {
                x.HasKey(e => e.IdVenta);
                x.Property(e => e.IdVenta).
                UseIdentityColumn()
                .ValueGeneratedOnAdd();
                x.ToTable("VENTA");
                x.Property(e => e.Fecha).HasColumnType("datetime");
                x.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Productos>(x =>
            {
                x.HasKey(e => e.Id);
                x.Property(e => e.Id).
                UseIdentityColumn()
                .ValueGeneratedOnAdd();
                x.ToTable("PRODUCTOS");
                x.Property(e => e.Nombre).HasMaxLength(50);
                x.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });


        }
    }

}
