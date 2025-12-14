using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySaaS.Application.Interfaces.Base;
using MySaaS.Application.Interfaces.Common.Items;
using MySaaS.Application.Interfaces.Common.Tenancy;
using MySaaS.Application.Interfaces.Common.Unities;
using MySaaS.Application.Interfaces.Production.Ingredients;
using MySaaS.Application.Interfaces.Production.Recipes;
using MySaaS.Application.Interfaces.Products.Products;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Database.Tenancy;
using MySaaS.Infrastructure.Repositories;

namespace MySaaS.Infrastructure
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Master");
            if (connectionString is null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddScoped<ITenantContext, TenantContext>();
            services.AddScoped<ITenantResolver>(sp => new TenantResolver(connectionString));
            services.AddScoped<DapperUnitOfWork>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<DapperUnitOfWork>());

            services.AddScoped<IDapperContext>(sp => sp.GetRequiredService<DapperUnitOfWork>());



            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUnitConversionRepository, UnitConversionRepository>();

            return services;
        }
    }
}
