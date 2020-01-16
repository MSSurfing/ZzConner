using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.EntityMapping.Metadata
{
    public class AssemblyMap : IEntityTypeConfiguration<Assembly>
    {
        public void Configure(EntityTypeBuilder<Assembly> builder)
        {
            builder.ToTable(TableNames.AssemblyTable);
            builder.HasKey(u => u.Id);

            builder.HasOne(e => e.FileInfo)
                .WithMany()
                .HasForeignKey(key => key.FileInfoId)
                .IsRequired();
        }
    }
}
