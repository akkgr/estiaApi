using System;
using estiaApi.Models;
using estiaApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace estiaApi
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
            services.AddHttpContextAccessor();
            services.AddScoped<CurrentUserService>();

            services.Configure<EstiaDatabaseSettings>(
                Configuration.GetSection(nameof(EstiaDatabaseSettings)));

            services.AddSingleton<IEstiaDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<EstiaDatabaseSettings>>().Value);

            var client = new MongoClient(Configuration.GetValue<string>("EstiaDatabaseSettings:ConnectionString"));
            services.AddSingleton<IMongoClient>(client);
            services.AddScoped<BuildingService>();

            services.AddCors(options =>
                    {
                        options.AddPolicy("_myAllowSpecificOrigins",
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:3000");
                            builder.AllowAnyHeader();
                            builder.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
                        });
                    });
            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:4000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "estiaApi";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("_myAllowSpecificOrigins");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
