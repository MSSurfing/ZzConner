using System.Collections.Generic;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    public class ResponseModel : ITypeModel
    {
        public ResponseModel()
        {
            Properties = new List<Property>();
        }

        public Method MethodInfo { get; set; }

        // Todo, use valid properties(Or Assert Properties)
        public virtual ICollection<Property> Properties { get; set; }
    }
}
