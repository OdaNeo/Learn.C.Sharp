using Learn.C.Sharp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.C.Sharp.ModelConfig
{
    public class TouristRouteConfig : IEntityTypeConfiguration<TouristRoute>
    {
        public void Configure(EntityTypeBuilder<TouristRoute> builder)
        {
            builder
                .Property(tr => tr.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
