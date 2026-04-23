using Microsoft.EntityFrameworkCore;
using SistemaEPI.Models;

namespace SistemaEPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Epi> Epis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EntregaEpi> EntregasEpi { get; set; }
        public DbSet<Treinamento> Treinamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=banco_epis.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
            modelBuilder.Entity<Epi>().HasQueryFilter(e => e.DeletedAt == null);

            modelBuilder.Entity<Funcionario>().HasQueryFilter(f => f.DeletedAt == null);

            modelBuilder.Entity<EntregaEpi>().HasQueryFilter(x => x.DeletedAt == null);

            modelBuilder.Entity<Treinamento>().HasQueryFilter(x => x.DeletedAt == null);
            
            modelBuilder.Entity<Usuario>().HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}