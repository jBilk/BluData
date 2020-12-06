
using ContatosVirtual.Migrations;
using System.Data.Entity;

namespace ContatosVirtual.Models
{
    public class ContatosVirtualContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public ContatosVirtualContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContatosVirtualContext, Configuration>());
        }
    }
}