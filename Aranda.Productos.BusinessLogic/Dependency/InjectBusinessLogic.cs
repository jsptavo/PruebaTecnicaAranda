using Aranda.Productos.BusinessLogic.Interfaces;
using Aranda.Productos.BusinessLogic.Logic;
using Aranda.Productos.Persistence.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aranda.Productos.BusinessLogic.Dependency          
{
    public class InjectBusinessLogic
    {
        public static void Inject(IServiceCollection services, IConfiguration configuration)
        {
            InjectPersistence.Inject(services, configuration);            
            services.AddTransient<IProductoLogic, ProductoLogic>();
        }
    }
}
