using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UGOCPBackEnd2019.ModelsSatProductos
{
    public partial class sat_productosContext : DbContext
    {
        public sat_productosContext()
        {
        }

        public sat_productosContext(DbContextOptions<sat_productosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<F4CAduana> F4CAduana { get; set; }
        public virtual DbSet<F4CCaducidadfolios> F4CCaducidadfolios { get; set; }
        public virtual DbSet<F4CClaveprodserv> F4CClaveprodserv { get; set; }

        // Unable to generate entity type for table 'dbo.f4_c_claveunidad'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_codigopostal'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_formapago'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_impuesto'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_metodopago'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_moneda'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_numpedimentoaduana'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_pais'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_patenteaduanal'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_regimenfiscal'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_tasaocuota'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_tipodecomprobante'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_tipofactor'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_tiporelacion'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.f4_c_usocfdi'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=sat_productos;Trusted_Connection=True;User Id=gonzo;Password=2185021;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<F4CAduana>(entity =>
            {
                entity.ToTable("f4_c_aduana");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CAduana)
                    .HasColumnName("c_Aduana")
                    .HasMaxLength(255);

                entity.Property(e => e.Descripción).HasMaxLength(255);
            });

            modelBuilder.Entity<F4CCaducidadfolios>(entity =>
            {
                entity.ToTable("f4_c_caducidadfolios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiasCaducidad).HasColumnName("diasCaducidad");

                entity.Property(e => e.PorcentajeCaducidad).HasColumnName("porcentajeCaducidad");
            });

            modelBuilder.Entity<F4CClaveprodserv>(entity =>
            {
                entity.ToTable("f4_c_claveprodserv");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CClaveProdServ)
                    .HasColumnName("c_ClaveProdServ")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementoQueDebeIncluir)
                    .HasColumnName("Complemento que debe incluir")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Descripción)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFinVigencia)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicioVigencia)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IncluirIepsTrasladado)
                    .HasColumnName("Incluir IEPS trasladado")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IncluirIvaTrasladado)
                    .HasColumnName("Incluir IVA trasladado")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PalabrasSimilares)
                    .HasColumnName("Palabras similares")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
