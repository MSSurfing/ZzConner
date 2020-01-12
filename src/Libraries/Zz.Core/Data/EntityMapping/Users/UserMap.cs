using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zz.Core.Data.Entity.Users;

namespace Zz.Core.Data.EntityMapping.Users
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.UserTable);
            builder.HasKey(u => u.Id);
        }
    }
}
