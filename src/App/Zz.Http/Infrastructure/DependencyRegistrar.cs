﻿using Autofac;
using Autofac.Engine;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Engine;
using Zz.Core;
using Zz.Core.Caching;
using Zz.Core.Mock;
using Zz.Core.Mock.Conner;
using Zz.Services.Authentication;
using Zz.Services.Authentication.OAuth;
using Zz.Services.Grpc;
using Zz.Services.Installers;
using Zz.Services.Media;
using Zz.Services.Metadata;
using Zz.Services.Users;
using Zz.Services.Users.External;

namespace Zz.Http.Core.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            // microsoft..mvc register
            builder.RegisterType<ApiContext>().As<IApiContext>().InstancePerLifetimeScope();
            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();

            // entity frameowkr register
            builder.Register<IDbContext>(e => new EfeObjectContext(e.Resolve<DbContextOptions<EfeObjectContext>>())).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfeRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // service register
            builder.RegisterType<InstallerService>().As<IInstallerService>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<ExternalUserService>().As<IExternalUserService>().InstancePerLifetimeScope();

            builder.RegisterType<OpenAuthenticationService>().As<IOpenAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ZzAuthenticationService>().As<IZzAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationOAuthProvider>().As<IOAuthProvider>().InstancePerLifetimeScope();

            builder.RegisterType<ServiceInfoService>().As<IServiceInfoService>().InstancePerLifetimeScope();

            builder.RegisterType<AssemblyService>().As<IAssemblyService>().InstancePerLifetimeScope();
            builder.RegisterType<FileInfoService>().As<IFileInfoService>().InstancePerLifetimeScope();

            // memory cache
            builder.RegisterType<SurfMemoryCache>().As<ICacheManager>().InstancePerLifetimeScope();

            // Mock & Conner
            builder.RegisterType<XActivator>().As<IActivator>().InstancePerLifetimeScope();
            builder.RegisterType<SurfMocker>().As<IMocker>().InstancePerLifetimeScope();
            builder.RegisterType<XServerCall>().As<IServerCall>().InstancePerLifetimeScope();
            builder.RegisterType<XMetadataProvider>().As<IMetadataProvider>().InstancePerLifetimeScope();

        }
    }
}
