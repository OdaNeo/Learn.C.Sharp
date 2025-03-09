using learn_c_sharp.Moldes;
using Microsoft.EntityFrameworkCore;

namespace learn_c_sharp.Database
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TouristRoute> TouristRouts { set; get; }
        public DbSet<TouristRoutePicture> touristRoutePictures { set; get; }
    }
}
