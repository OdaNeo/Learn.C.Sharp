using Learn.C.Sharp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.C.Sharp.ModelConfig
{
    public class TouristRoutePictureConfig : IEntityTypeConfiguration<TouristRoutePicture>
    {
        public void Configure(EntityTypeBuilder<TouristRoutePicture> builder)
        {
            builder
                .HasOne(p => p.TouristRoute)
                .WithMany(r => r.TouristRoutePictures)
                .HasForeignKey(p => p.TouristRouteId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
