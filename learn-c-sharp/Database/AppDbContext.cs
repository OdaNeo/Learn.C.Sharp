using learn_c_sharp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

namespace learn_c_sharp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TouristRoute> TouristRouts { set; get; }
        public DbSet<TouristRoutePicture> TouristRoutePictures { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var touristRouteJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                @"/Database/touristRoutesMockData.json");
            IList<TouristRoute> touristRoutes = JsonConvert.DeserializeObject<IList<TouristRoute>>(touristRouteJsonData)!;
            modelBuilder.Entity<TouristRoute>().HasData(touristRoutes);
            modelBuilder.Entity<TouristRoute>()
               .Property(tr => tr.CreateTime)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

            var touristRoutePictureJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                @"/Database/touristRoutePicturesMockData.json");
            IList<TouristRoutePicture> touristPictureRoutes = JsonConvert.DeserializeObject<IList<TouristRoutePicture>>(touristRoutePictureJsonData)!;
            modelBuilder.Entity<TouristRoutePicture>().HasData(touristPictureRoutes);

            base.OnModelCreating(modelBuilder);
        }
    }
}
