using System.Collections.Generic;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    public class ServiceInfo : ITypeModel
    {
        private ICollection<Method> _methods;

        public ServiceInfo()
        {
            
        }

        // 用于获取 Service所在程序集的 实体、枚举等对象
        //public virtual Assembly Assembly { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Method> Methods
        {
            get => _methods ?? (_methods = new List<Method>());
            protected set => _methods = value;
        }
    }
}
