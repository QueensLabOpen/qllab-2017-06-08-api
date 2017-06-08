using Microsoft.EntityFrameworkCore;
using QLLAB.Models;

namespace QLLAB.Data
{
    public class QlLabContext : DbContext
    {
        public DbSet<Image> Images { get; set; }

        public QlLabContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>();
        }
    }
}
