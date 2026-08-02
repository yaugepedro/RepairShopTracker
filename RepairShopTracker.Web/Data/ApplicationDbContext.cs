using Microsoft.EntityFrameworkCore;
using RepairShopTracker.Web.Models;

namespace RepairShopTracker.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RepairOrder> RepairOrders { get; set; }
        public DbSet<LoginUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoginUser>().HasData(
                new LoginUser
                {
                    Id = 1,
                    Username = "admin",
                    Password = "Admin123!"
                }
            );
        }
    }
}