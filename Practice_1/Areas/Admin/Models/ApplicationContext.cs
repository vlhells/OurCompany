using Microsoft.EntityFrameworkCore;
using Practice_1.Areas.Admin.Models;

namespace Practice_1.Admin.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated(); // Создаем бд при первом обращении.
        }
    }
}
