using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Entities;
using UGOCPBackEnd2019.Models;

namespace UGOCPBackEnd2019.Data
{
    public class UgocpDbContext : IdentityDbContext<User, Role, Guid>
    {
        public UgocpDbContext(DbContextOptions<UgocpDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamar a la clase Identity de la que heredo.
            base.OnModelCreating(modelBuilder);

            // Configure User.
            modelBuilder.Entity<User>(ConfigureUser);
            // Configure Company.
            modelBuilder.Entity<Company>(ConfigureCompany);
            // Configure Product.
            modelBuilder.Entity<Product>(ConfigureProduct);
        }

        // Funciones para configurar las entidadas.
        public void ConfigureCompany(EntityTypeBuilder<Company> builder)
        {
            // Define primary key.
            builder.HasKey(c => c.IdCompany);

            builder.HasMany(p => p.LstProduct);
        }
        public void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.LstCompany);
        }
        public void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.IdProduct);

            
        }
    }
}
