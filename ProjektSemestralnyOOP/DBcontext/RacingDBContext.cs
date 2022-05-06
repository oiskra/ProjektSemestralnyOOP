using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.MVVM.Model;

namespace ProjektSemestralnyOOP.DBcontext
{
    public class RacingDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Car> Market { get; set; }

        public RacingDBContext(DbContextOptions<RacingDBContext> options) : base(options)
        { }
    }
}
