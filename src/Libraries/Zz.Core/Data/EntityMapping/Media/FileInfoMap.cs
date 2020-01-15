using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zz.Core.Data.Entity.Media;

namespace Zz.Core.Data.EntityMapping.Media
{
    public class FileInfoMap : IEntityTypeConfiguration<FileInfo>
    {
        public void Configure(EntityTypeBuilder<FileInfo> builder)
        {
            builder.ToTable(TableNames.FileInfoTable);
            builder.HasKey(u => u.Id);
        }
    }
}
