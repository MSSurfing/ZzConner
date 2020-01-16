using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Data.EntityMapping.Grpc
{
    public class MethodMap : IEntityTypeConfiguration<Method>
    {
        public void Configure(EntityTypeBuilder<Method> builder)
        {
            builder.ToTable(TableNames.MethodTable);
            builder.HasKey(u => u.Id);

            builder.HasOne(e => e.ServiceInfo)
                .WithMany(e => e.Methods)
                .HasForeignKey(k => k.ServiceInfoId)
                .IsRequired();

            // 错误索引
            //builder.HasIndex(e => e.ServiceInfoId)
            //    .HasName(null)
            //    .IsUnique();

            // 一对一关系（且RequestModel中不需要MethodId的属性）
            builder.HasOne(e => e.RequestModel)
                .WithOne(e => e.MethodInfo)
                .HasPrincipalKey<RequestModel>(e => e.Id)
                .HasForeignKey<Method>(k => k.RequestModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName(null);

            // 一对一关系（且ResponseModel中不需要MethodId的属性）
            builder.HasOne(e => e.ResponseModel)
                .WithOne(e => e.MethodInfo)
                .HasPrincipalKey<ResponseModel>(e => e.Id)
                .HasForeignKey<Method>(k => k.ResponseModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName(null);
        }
    }
}
