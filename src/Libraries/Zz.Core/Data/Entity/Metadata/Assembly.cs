using System;
using Zz.Core.Data.Entity.Media;

namespace Zz.Core.Data.Entity.Metadata
{
    public class Assembly : BaseEntity
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public Guid FielInfoId { get; set; }

        public bool Deleted { get; set; }

        //

        #region Nav properties
        public virtual FileInfo FileInfo { get; set; }
        #endregion
        // Todo
    }
}
