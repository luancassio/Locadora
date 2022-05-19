using ApiLocadora.Application.Contracts;
using ApiLocadora.Persistence;
using ApiLocadora.Persistence.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ApiLocadora.API
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRentalCompanyService, RentalCompanyService>();

            services.AddScoped<IGeralPersist, GeralPersist>();
            services.AddScoped<IClientPersist, ClientPersist>();
            services.AddScoped<IFilmPersist, FilmPersist>();
            services.AddScoped<IRentalCompanyPersist, RentalCompanyPersist>();
            services.AddDbContext<ApiLocadoraContext>(
               context => context.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
           );
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "ApiLocadora", 
                    Version = "v1",                  
                    Contact = new OpenApiContact
                    {
                        Name = "Luan Cássio",
                        Email = "luancassio2307@gmail.com"                    
                    }                   
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiLocadora v1"));
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
