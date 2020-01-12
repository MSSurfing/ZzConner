using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zz.Core.Data.EntityQueries.Strings
{
    public class StringItems
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class StringItemsMap : IQueryTypeConfiguration<StringItems>
    {
        public void Configure(QueryTypeBuilder<StringItems> builder)
        {

        }
    }
}
