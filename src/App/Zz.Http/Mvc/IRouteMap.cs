using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zz.Http.Core.Mvc
{
    public partial interface IRouteMap
    {
        void Map(IRouteBuilder routeBuilder);

        int Order { get; }
    }
}
