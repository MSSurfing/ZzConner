using System;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    public class Method : IMetadata
    {
        #region Properties
        public Guid ServiceInfoId { get; set; }
        public Guid RequestModelId { get; set; }
        public Guid ResponseModelId { get; set; }
        #endregion

        #region Nav properties
        /// <summary>
        /// 所属服务
        /// </summary>
        public virtual ServiceInfo ServiceInfo { get; set; }

        //public virtual ICollection<MessageModel> Parameters { get; set; }

        public virtual RequestModel RequestModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ResponseModel ResponseModel { get; set; }
        #endregion
    }
}
