using System.Collections.Generic;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    public class Service : ITypeModel
    {
        /*private readonly ICollection<MethodModel> _methods;*/

        public Service()
        {
            Methods = new List<Method>();
        }

        // 用于获取 Service所在程序集的 实体、枚举等对象
        //public virtual Assembly Assembly { get; set; }

        public virtual ICollection<Method> Methods { get; set; }
    }
}
