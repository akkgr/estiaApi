using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using estiaApi.Models;
using estiaApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace estiaApi
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
            services.Configure<EstiaDatabaseSettings>(
                Configuration.GetSection(nameof(EstiaDatabaseSettings)));

            services.AddSingleton<IEstiaDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<EstiaDatabaseSettings>>().Value);

            services.AddSingleton<BuildingService>();

            services.AddCors(options =>
                    {
                        options.AddPolicy("_myAllowSpecificOrigins",
                        builder =>
                        {
                            builder.AllowAnyOrigin();
                            builder.AllowAnyHeader();
                            // builder.WithExposedHeaders("Content-Range");
                            builder.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
                        });
                    });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // using Microsoft.AspNetCore.HttpOverrides;

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("_myAllowSpecificOrigins");

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
