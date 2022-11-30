using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShortUrl.Models;
using ShortUrl.Services.Implementations;
using ShortUrl.Services.Interfaces;
using ShortUrl.Settings;

namespace ShortUrl
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
            
            services.Configure<QrCodeOptions>(Configuration.GetSection(QrCodeOptions.Position));
            
            services.AddDbContext<ShortUrlContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ShortUrl")));

            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IQrCodeService, QrCodeService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShortUrl", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShortUrl v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{*url}",
                    new { controller = "Redirect", action = "Redirect" }
                );
            });
        }
    }
}