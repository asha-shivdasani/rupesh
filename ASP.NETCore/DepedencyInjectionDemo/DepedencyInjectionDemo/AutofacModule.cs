using Autofac;
using DepedencyInjectionDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepedencyInjectionDemo
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EntityDataManager>().As<IDataManager>().SingleInstance();
            builder.RegisterType<ADODataManager>().As<IDataManager>().SingleInstance();
            builder.RegisterType<DemoService>().As<DemoService>();
        }
    }
}
