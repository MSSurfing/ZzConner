using System.Collections.Generic;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    public class RequestModel : ITypeModel
    {
        public RequestModel()
        {
            Properties = new List<Property>();
        }

        public Method MethodInfo { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
