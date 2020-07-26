using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;
using Autofac;
using Autofac.Integration.WebApi;
using Stories.WebApi.Controllers;
using Stories.WebApi.Models;
using Stories.Service;
using Stories.Repository;
using Stories.Model;

namespace Stories.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<UserController>();
            containerBuilder.RegisterType<StoryController>();
            containerBuilder.RegisterModule<ServiceDIModule>();
            containerBuilder.RegisterModule<RepositoryDIModule>();

            /*automapper:*/
            containerBuilder.RegisterType<UserModel>().AsSelf();
            containerBuilder.RegisterType<StoryModel>().AsSelf();

            containerBuilder.RegisterType<User>().AsSelf();
            containerBuilder.RegisterType<Story>().AsSelf();
            

            containerBuilder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>().ReverseMap();
                cfg.CreateMap<Story, StoryModel>().ReverseMap();

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
