using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Autofac;
using Autofac.Integration.WebApi;
using Stories.WebAPI.Controllers;
using Stories.WebAPI.Models;
using Stories.Service;
using Stories.Repository;
using Stories.Model;

namespace Stories.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<GenreController>();
            containerBuilder.RegisterModule<ServiceDIModule>();
            containerBuilder.RegisterModule<RepositoryDIModule>();

            /*automapper:*/
            containerBuilder.RegisterType<GenreModel>().AsSelf();

            containerBuilder.RegisterType<Genre>().AsSelf();


            containerBuilder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genre, GenreModel>().ReverseMap();

            })).AsSelf().SingleInstance();

            containerBuilder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();/*:automapper*/

            var container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }
    }
}
