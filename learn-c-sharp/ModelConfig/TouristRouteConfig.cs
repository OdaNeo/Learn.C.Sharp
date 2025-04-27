using learn_c_sharp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace learn_c_sharp.ModelConfig
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
