using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Data.EntityMapping.Grpc
{
    public class ResponseModelMap : IEntityTypeConfiguration<ResponseModel>
    {
        public void Configure(EntityTypeBuilder<ResponseModel> builder)
        {
            builder.ToTable(TableNames.ResponseModelTable);
            builder.HasKey(u => u.Id);
        }
    }
}
