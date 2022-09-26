using Aranda.Productos.Persistence.Data;
using Aranda.Productos.Persistence.Interfaces;
using Aranda.Productos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aranda.Productos.Persistence.Dependency
{
    public class InjectPersistence
    {
        public static void Inject(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<IProductoRepositorio, ProductoRepositorio>();
            
            services.AddDbContext<CatalagoArandaDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("ArandaConnection")));
        }
    }
}