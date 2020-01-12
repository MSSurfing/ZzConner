using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Engine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Zz.Http.Core.Infrastructure.Extensions;
using Zz.Http.Core.Validators;

namespace Zz.Http.Core.Hosting
{
    public static class StartupExtensions //HttpServiceCollectionExtensions
    {
        #region IApplication builder extensions
        public static void ZzHostingConfigure(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseRouteMap();
            app.UseStaticFiles(env);
        }
        #endregion

        #region IService Collection Extensions
        public static void AddZzMvc(this IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc();

            mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            mvcBuilder.AddFluentValidation(config =>
            {
                config.ValidatorFactoryType = typeof(ValidatorFactory);
                config.ImplicitlyValidateChildProperties = true;
            });
        }

        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<EfeObjectContext>(optionsAction =>
            {
                optionsAction.UseSqlServerOption(services);
            });
        }

        // AddIHostingEnvironment(this IServiceCollection services)

        public static void AddZzHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        //public static void AddCompositeFileProvider(this IServiceCollection services)
        //{
        //    //var physicalProvider = hostingEnvironment.ContentRootFileProvider;
        //    var embeddedProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());

        //    services.AddSingleton<IFileProvider>(new CompositeFileProvider(embeddedProvider));
        //}
        #endregion
    }
}
