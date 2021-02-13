using EjemploCache.Usuarios.External.Empresa.Services.Distributed;
using EjemploCache.Usuarios.External.Empresa.Services.Memory;
using EjemploCache.Usuarios.Repositories;
using EjemploCache.Usuarios.ServiceDependencies;
using EjemploCache.Usuarios.ServiceDependencies.WithDistributedCache;
using EjemploCache.Usuarios.Services;
using EjemploCache.Usuarios.Services.WithCache;
using EjemploCache.Usuarios.Services.WithDistributedCache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace EjemploCache.Usuarios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ListUsersWithCompanyName>();
            services.AddScoped<ListUsersWithMemoryCache>();
            services.AddScoped<ListUsersWithDistributedCache>();
            services.AddScoped<IListUsersWithCompanyNameDependencies, ListUsersWithCompanyNameDependencies>();
            services.AddScoped<IListUsersWithMemoryCache, ListUsersMemoryCacheServiceDependencies>();
            services.AddScoped<IListUsersWithDistributedCache, ListUsersDistributedCacheServiceDependencies>();
            services.AddScoped<IDistributedEmpresaServicio, External.Empresa.Services.Distributed.EmpresaServicio>();
            services.AddSingleton<IEmpresaServicio, External.Empresa.Services.Memory.EmpresaServicio>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost:6380,password=password123";
                options.InstanceName = "localhost";
            });

            services.AddHttpClient("EmpresaMS", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44382/");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EjemploCache.Usuarios", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EjemploCache.Usuarios v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
