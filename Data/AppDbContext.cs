
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Models;
namespace MoveisMedeiros.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Inspecao> Inspecoes { get; set; }
        public DbSet<ItemObra> ItensObra { get; set; }
        public DbSet<Checklist> Checklist { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Historico> Historicos { get; set; }
    }
}
