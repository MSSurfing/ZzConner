using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Data.EntityMapping.Grpc
{
    public class ServiceInfoMap : IEntityTypeConfiguration<ServiceInfo>
    {
        public void Configure(EntityTypeBuilder<ServiceInfo> builder)
        {
            builder.ToTable(TableNames.ServiceInfoTable);
            builder.HasKey(u => u.Id);


            builder.HasOne(e => e.Assembly)
                .WithMany(e => e.ServiceInfos)
                .HasForeignKey(k => k.AssemblyId)
                .IsRequired();
        }
    }
}
