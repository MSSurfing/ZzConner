using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zz.Http.Core.Hosting;

namespace Zz.Http.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            MvcServiceCollectionExtensions.AddMvc(services);

            services.AddHttpContextAccessor();
            services.AddZzMvc();

            services.AddDbContext();
            services.AddEntityFrameworkSqlServer();
            services.AddEntityFrameworkProxies();

            services.AddSwaggerGen(e =>
            {
                e.SwaggerDoc("api", new Swashbuckle.AspNetCore.Swagger.Info { Title = "api", Version = "v1" });
            });

            //services.AddCompositeFileProvider();

            return EngineContext.Initialize(services, ScopeTag.Http);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.ZzHostingConfigure(env);

            app.UseSwagger();
            app.UseSwaggerUI(e => { });
        }
    }
}
