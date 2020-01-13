using Autofac.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zz.Http.Core.Mvc;

namespace Zz.Http.Core.Hosting
{
    public static class UseExtensions
    {
        public static void UseStaticFiles(this IApplicationBuilder application, IWebHostEnvironment env)
        {
            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/Views"),
                RequestPath = new PathString("/Views"),
            });

            //application.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/Assets"),
            //    RequestPath = new PathString("/Assets"),
            //});
        }

        public static void UseRouteMap(this IApplicationBuilder application)
        {
            application.UseMvc(routeBuilder =>
            {
                var typeFinder = EngineContext.Resolve<ITypeFinder>();
                var routeMaps = typeFinder.FindClassesOfType<IRouteMap>().Select(e => (IRouteMap)Activator.CreateInstance(e));

                foreach (var routeMap in routeMaps.OrderBy(e => e.Order))
                {
                    routeMap.Map(routeBuilder);
                }
            });
        }
    }
}
