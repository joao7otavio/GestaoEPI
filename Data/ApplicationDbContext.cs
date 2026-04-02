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
    }
}