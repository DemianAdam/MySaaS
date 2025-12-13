using Microsoft.Extensions.DependencyInjection;
using MySaaS.Application.Interfaces.Common.Unities;
using MySaaS.Application.Interfaces.Production.Ingredients;
using MySaaS.Application.Interfaces.Production.Recipes;
using MySaaS.Application.Interfaces.Products.Products;
using MySaaS.Application.Services;


namespace MySaaS.Application
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUnitService, UnitService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IUnitConversionService, UnitConversionService>();
            return services;
        }
    }
}
