using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UGOCPBackEnd2019
{
    public partial class cat_localidadContext : DbContext
    {
        public cat_localidadContext()
        {
        }

        public cat_localidadContext(DbContextOptions<cat_localidadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Localidades> Localidades { get; set; }
        public virtual DbSet<Municipios> Municipios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=cat_localidad;User Id=gonzo;Password=2185021;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Estados>(entity =>
            {
                entity.ToTable("estados", "cat_localidad");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abrev)
                    .IsRequired()
                    .HasColumnName("abrev")
                    .HasMaxLength(10);

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(2);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Localidades>(entity =>
            {
                entity.ToTable("localidades", "cat_localidad");

                entity.HasIndex(e => e.MunicipioId)
                    .HasName("municipio_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Altitud)
                    .IsRequired()
                    .HasColumnName("altitud")
                    .HasMaxLength(15);

                entity.Property(e => e.Ambito)
                    .IsRequired()
                    .HasColumnName("ambito")
                    .HasMaxLength(1);

                entity.Property(e => e.Carta)
                    .IsRequired()
                    .HasColumnName("carta")
                    .HasMaxLength(10);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(4);

                entity.Property(e => e.Femenino).HasColumnName("femenino");

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasColumnType("decimal(10, 7)");

                entity.Property(e => e.Latitud)
                    .IsRequired()
                    .HasColumnName("latitud")
                    .HasMaxLength(15);

                entity.Property(e => e.Lng)
                    .HasColumnName("lng")
                    .HasColumnType("decimal(10, 7)");

                entity.Property(e => e.Longitud)
                    .IsRequired()
                    .HasColumnName("longitud")
                    .HasMaxLength(15);

                entity.Property(e => e.Masculino).HasColumnName("masculino");

                entity.Property(e => e.MunicipioId).HasColumnName("municipio_id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100);

                entity.Property(e => e.Poblacion).HasColumnName("poblacion");

                entity.Property(e => e.Viviendas).HasColumnName("viviendas");
            });

            modelBuilder.Entity<Municipios>(entity =>
            {
                entity.ToTable("municipios", "cat_localidad");

                entity.HasIndex(e => e.EstadoId)
                    .HasName("estado_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(3);

                entity.Property(e => e.EstadoId).HasColumnName("estado_id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100);
            });
        }
    }
}
