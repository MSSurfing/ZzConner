using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zz.Core.Data.Entity.Media
{
    public class FileInfo : BaseEntity
    {
        public string Extension { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public bool Deleted { get; set; }
    }
}
