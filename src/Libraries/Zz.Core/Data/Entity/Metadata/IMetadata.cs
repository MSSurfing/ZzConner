using System;

namespace Zz.Core.Data.Entity.Metadata
{
    // Zz DG中的 IModel
    public abstract class IMetadata : BaseEntity
    {
        //public virtual Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
    }
}
