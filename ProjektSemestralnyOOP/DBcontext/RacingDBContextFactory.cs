using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjektSemestralnyOOP.DBcontext
{
    /// <summary>
    /// Class for creating of database context. 
    /// </summary>
    public class RacingDBContextFactory : IDesignTimeDbContextFactory<RacingDBContext>
    {
        /// <summary>
        /// Method for creating <see cref="RacingDBContext"/> instance.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>New instance of <see cref="RacingDBContext"/></returns>
        public RacingDBContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RacingDBContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RacingDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new RacingDBContext(optionsBuilder.Options);
        }
    }
}
