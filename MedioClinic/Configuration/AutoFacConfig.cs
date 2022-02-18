using Autofac;
using Core;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Reflection;
using XperienceAdapter.Localization;
using XperienceAdapter.Repositories;

namespace MedioClinic.Configuration
{
	public class AutoFacConfig
	{
		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
				.Where(type => type.IsClass && !type.IsAbstract && typeof(IService).IsAssignableFrom(type))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
				.Where(type => type.GetTypeInfo()
					.ImplementedInterfaces.Any(
						@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IRepository<>)))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
				.Where(type => type.GetTypeInfo()
					.ImplementedInterfaces.Any(
						@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IPageRepository<,>)))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			//Register the factory in AutoFac
			builder.RegisterType<XperienceStringLocalizerFactory>()
				.As<IStringLocalizerFactory>()
				.InstancePerLifetimeScope();
		}
	}

}
