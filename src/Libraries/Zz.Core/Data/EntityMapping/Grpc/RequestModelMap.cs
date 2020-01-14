using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Data.EntityMapping.Grpc
{
    public class RequestModelMap : IEntityTypeConfiguration<RequestModel>
    {
        public void Configure(EntityTypeBuilder<RequestModel> builder)
        {
            builder.ToTable(TableNames.RequestModelTable);
            builder.HasKey(u => u.Id);
        }
    }
}
