using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<DeliveryPerson> DeliveryPerson { get; set; }
        public DbSet<DeliveryRequest> DeliveryRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
