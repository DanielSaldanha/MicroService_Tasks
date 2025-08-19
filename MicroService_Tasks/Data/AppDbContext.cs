using MicroService_Tasks.Model;
using Microsoft.EntityFrameworkCore;
namespace MicroService_Tasks.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Tarefa> Lista { get; set; } 
    }
}
