using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zz.Core.Data.Entity.Metadata
{
    public class Assembly : BaseEntity
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        // Todo
    }
}
