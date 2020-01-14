using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Data.EntityMapping.Grpc
{
    public class PropertyMap : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable(TableNames.PropertyTable);
            builder.HasKey(u => u.Id);

            builder.HasOne(e => e.MethodInfo)
                .WithMany()
                .HasForeignKey(k => k.MethodId)
                .IsRequired();

            builder.HasOne(e => e.RequestModel)
                .WithMany(p => p.Properties)
                .HasForeignKey(k => k.RequestModelId);

            builder.HasOne(e => e.ResponseModel)
                .WithMany(p => p.Properties)
                .HasForeignKey(k => k.ResponseModelId);

            builder.HasOne(e => e.PropertyValue)
                .WithMany()
                .HasForeignKey(k => k.PropertyValueId);

            builder.Ignore(e => e.DataType);
            builder.Ignore(e => e.ValueType);
        }
    }
}
