using AuthenticationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Context
{
    public class DBContext : DbContext
    {
       public DBContext(DbContextOptions<DBContext> options) : base(options) {}

        public DbSet<Veiculo> Veiculos { get; set; } = default!;

        public DbSet<Administrador> Administradores { get; set; } = default!;

    }
}
