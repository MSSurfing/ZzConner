using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zz.Core.Data
{
    public class StringValue
    {
        public string Value { get; set; }
    }

    public class StringDataMap : IQueryTypeConfiguration<StringValue>
    {
        public void Configure(QueryTypeBuilder<StringValue> builder)
        {

        }
    }
}
