using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Warehouse.PersistenceEF;
using Warehouse.PersistenceEF.Categories;
using Warehouse.PersistenceEF.NeedRequests;
using Warehouse.PersistenceEF.Products;
using Warehouse.Services.Categories;
using Warehouse.Services.Categories.Contracts;
using Warehouse.Services.Products;
using Warehouse.Services.Products.Contracts;
using Warehouse.Services.RequestNeeds;
using Warehouse.Services.RequestNeeds.Contracts;
using Warehouse.Services.SharedContracts;

namespace Warehouse
{
    public class Startup
    {
        private readonly string _connectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = configuration.GetConnectionString("stage");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            ConfigBusinessServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigBusinessServices(IServiceCollection services)
        {
            services.AddScoped<EFDataContext>(_ => new EFDataContext(_connectionString));

            services.AddScoped<ProductService, ProductAppService>();
            services.AddScoped<CategoryService, CategoryAppService>();
            services.AddScoped<RequestNeedService, RequestNeedAppService>();

            services.AddScoped<ProductRepository, EFProductRepository>();
            services.AddScoped<CategoryRepository, EFCategoryRepository>();
            services.AddScoped<RequestNeedRepository, EFRequestNeedRepository>();

            services.AddTransient<UnitOfWork, EFUnitOfWork>();
        }
    }
}
