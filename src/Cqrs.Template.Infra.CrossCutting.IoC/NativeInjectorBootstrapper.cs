using System;
using Cqrs.Template.Application.Behaviors;
using Cqrs.Template.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Template.Infra.CrossCutting.IoC;

public static class NativeInjectorBootstrapper
{
	public static void RegisterServices(IServiceCollection services)
	{
		RegisterData(services);
		RegisterMediatR(services);
	}

	private static void RegisterData(IServiceCollection services)
	{
		services.AddMemoryCache();
		// here goes your repository injection
		// sample: services.AddScoped<IUserRepository, UserRepository>();
	}

	private static void RegisterMediatR(IServiceCollection services)
	{
		const string applicationAssemblyName = "Cqrs.Template.Application"; // use your project name
		var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

		AssemblyScanner
			.FindValidatorsInAssembly(assembly)
			.ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

		// injection for Mediator
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));
		services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
	}
}
