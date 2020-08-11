﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Stories.Repository.Common;

namespace Stories.Repository
{
    public class RepositoryDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerDependency();
            builder.RegisterType<StoryRepository>().As<IStoryRepository>().InstancePerDependency();
            builder.RegisterType<ChapterRepository>().As<IChapterRepository>().InstancePerDependency();
        }
    }
}
