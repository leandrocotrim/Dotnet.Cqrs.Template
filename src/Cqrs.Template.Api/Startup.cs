using Cqrs.Template.Api.Filters;
using Cqrs.Template.Application.CommandHandlers;
using Cqrs.Template.Infra.CrossCutting.IoC.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cqrs.Template.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApiVersioning();
			services.AddVersionedApiExplorer();
			services.AddSwaggerSetup();
			services.AddAutoMapper();
			services.AddDependencyInjectionSetup();
			services.AddMediatR(typeof(CommandHandler));
			services.AddScoped<GlobalExceptionFilterAttribute>();
			services.AddDatabaseSetup();
			
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder =>
			{
				builder.WithOrigins("*");
				builder.AllowAnyOrigin();
				builder.AllowAnyMethod();
				builder.AllowAnyHeader();
			});
			

			app.UseRouting();

			app.UseAuthorization();
			app.UseSwaggerSetup(provider);

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}