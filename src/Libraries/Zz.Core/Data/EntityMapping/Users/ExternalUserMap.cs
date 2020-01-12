using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Zz.Core.Data.Entity.Users;

namespace Zz.Core.Data.EntityMapping.Users
{
    public partial class ExternalUserMap : IEntityTypeConfiguration<ExternalUser>
    {
        public void Configure(EntityTypeBuilder<ExternalUser> builder)
        {
            builder.ToTable(TableNames.ExternalUserMapping);
            builder.HasKey(u => u.Id);

            //Relationships
            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(k => k.UserId)
                .IsRequired();

            builder.HasOne(e => e.OpenAuthentication)
                .WithMany()
                .HasForeignKey(k => k.OpenAuthenticationId)
                .IsRequired();
        }
    }
}
