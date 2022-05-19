using ApiLocadora.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence
{
    public class ApiLocadoraContext : DbContext
    {
        public ApiLocadoraContext(DbContextOptions<ApiLocadoraContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<RentalCompany> RentalCompanys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(f => f.Id);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(a => a.Id);
            });

            modelBuilder.Entity<RentalCompany>(entity =>
            {
                entity.HasKey(r => r.Id);
            });
        }
             
    }
}
