using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PortfolioVisualizer {
    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddHttpClient();
            services.AddAutoMapper(GetType().Assembly);
            services.AddTransient<Service.IAssetTypeService, Service.AssetTypeService>();
            services.AddTransient<Service.IAssetService, Service.AssetService>();
            services.AddDbContext<Data.PortfolioDbContext>(t => {
                t.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                t.UseSqlServer(Configuration.GetConnectionString("Default"));
            }, ServiceLifetime.Scoped);
            services.AddEntityFrameworkSqlServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Data.PortfolioDbContext dbContext) {
            dbContext.Database.EnsureCreated();
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
