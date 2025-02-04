using Microsoft.EntityFrameworkCore;
using SensorAPP.Models;

namespace SensorAPP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SensorData> SensorData { get; set; }
    }
}
