using WebApiRedArbor.Interfaces;
using WebApiRedArbor.Repositories;
using WebApiRedArbor.Services;

namespace WebApiRedArbor.Extensiones
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInternalDependencies(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
        {
            serviceCollection
                .AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>))
                .AddScoped<IRepositoryEmployee, RepositoryEmployee>()
                .AddScoped<IServiceEmployee, ServiceEmployee>()
                .AddScoped<IServiceRole, ServiceRole>();

            return serviceCollection;
        }
    }
}
