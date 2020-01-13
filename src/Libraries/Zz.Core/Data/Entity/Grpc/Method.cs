using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    public class Method : IMetadata
    {
        /// <summary>
        /// 所属服务
        /// </summary>
        public Service ServiceInfo { get; set; }

        //public virtual ICollection<MessageModel> Parameters { get; set; }

        public virtual RequestModel RequestInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ResponseModel ResponseInfo { get; set; }
    }
}
