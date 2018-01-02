using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreProcessorTest.Handlers;
using PreProcessorTest.Messages;
using PreProcessorTest.Messages.Commands;
using PreProcessorTest.Pipeline;
using Serilog;

namespace PreProcessorTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //
            // AUTOFAC CONFIGURATION

            var builder = new ContainerBuilder();

            // Copies existing dependencies from IServiceCollection
            builder.Populate(services);

            // AUTOFAC MEDIATR CONFIG
            
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestHandler<>),
                typeof(INotificationHandler<>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                // Register all command handler in the same assembly as WriteLogMessageCommandHandler
                builder
                    .RegisterAssemblyTypes(typeof(MyCommandHandler).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();

                // Register all QueryHandlers in the same assembly as GetExternalLoginQueryHandler
                builder
                    .RegisterAssemblyTypes(typeof(MyQueryHandler).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            // Pipeline pre/post processors, from MediatR documentation
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));



            // THE LINE AT ISSUE - Runs for ALL requests. Need to find way to run for commands only.
            // SEE: https://stackoverflow.com/questions/48045989/executing-mediatr-preprocessor-only-for-specific-interface-types-commands

            builder.RegisterGeneric(typeof(MyCommandPreProcessor<>)).As(typeof(IRequestPreProcessor<>));




            // Additional registration options provided by MediatR documentation
            // builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            // builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            //// AUTOFAC SERILOG CONFIG

            //builder.Register<Serilog.ILogger>((c, p) =>
            //{
            //    return new LoggerConfiguration()
            //        .ReadFrom.Configuration(Configuration)
            //        .CreateLogger();
            //}).SingleInstance();

            // Finalize Autofac
            var container = builder.Build();

            //Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
