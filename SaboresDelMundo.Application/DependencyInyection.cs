using Microsoft.Extensions.DependencyInjection;
using MySaaS.Application.Interfaces.Products;
using MySaaS.Application.Interfaces.Recipes;
using MySaaS.Application.Interfaces.Supplies;
using MySaaS.Application.Interfaces.Supplies.Ingredients;
using MySaaS.Application.Interfaces.Unities;
using MySaaS.Application.Services;


namespace MySaaS.Application
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUnitService, UnitService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<ISupplyService, SupplyService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IProductService,ProductService>();
            return services;
        }
    }
}
