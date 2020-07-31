using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Stories.Service.Common;

namespace Stories.Service
{
    public class ServiceDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerDependency();
        }
    }
}
