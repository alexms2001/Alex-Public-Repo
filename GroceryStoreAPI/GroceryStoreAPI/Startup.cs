using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GroceryStoreAPI
{
    public class Startup
    {
        /// <summary>
        /// "Standard" out-of-the-box method.
        /// </summary>
        /// <param name="configuration">Implementation of IConfiguration interface (will be provided automagically).</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// "Standard" out-of-the-box property.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// "Standard" out-of-the-box method.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Implementation of IServiceCollection interface (will be provided automagically).</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IStoreRepository, StoreRepository>();
        }

        /// <summary>
        /// "Standard" out-of-the-box method.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Per compiler warning CS0618:
        ///     'IHostingEnvironment' is obsolete: 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        /// replaced IHostingEnvironment by IWebHostEnvironment:
        /// </summary>
        /// <param name="app">Implementation of IApplicationBuilder interface (will be provided automagically).</param>
        /// <param name="env">Implementation of IWebHostEnvironment interface (will be provided automagically).</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
