using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Data.Mapping.File
{
    public class FileMapper : IEntityTypeConfiguration<Entity.File>
    {
        public void Configure(EntityTypeBuilder<Entity.File> builder)
        {
            builder
                .HasMany(f => f.Shares)
                .WithOne(f => f.File)
                .HasForeignKey(fs => fs.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
