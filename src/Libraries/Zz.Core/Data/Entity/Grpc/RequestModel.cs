using System;
using System.Collections.Generic;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    public class RequestModel : ITypeModel
    {
        private ICollection<Property> _properties;

        public RequestModel()
        {

        }
        public RequestModel(ICollection<Property> properties)
        {
            _properties = properties;
        }

        public virtual Method MethodInfo { get; set; }

        public virtual ICollection<Property> Properties
        {
            get => _properties ?? (_properties = new List<Property>());
            protected set => _properties = value;
        }
    }
}
