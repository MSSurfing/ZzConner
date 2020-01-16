using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Mock
{
    // 应放在Services 层
    public interface IMetadataProvider
    {
        string GetTypeName(object obj);

        Type GetType(string typeName);

        List<ServiceInfo> GetServices(Assembly assembly);
    }
}
