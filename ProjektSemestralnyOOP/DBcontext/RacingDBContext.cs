using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.MVVM.Model;

namespace ProjektSemestralnyOOP.DBcontext
{
    public class RacingDBContext : DbContext
    {
        public RacingDBContext(DbContextOptions<RacingDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Car> Market { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RacingDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
