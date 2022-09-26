using Aranda.Productos.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.Persistence.Dependency
{
    public class ApplyDbMigrations
    {
        public static void Apply(IServiceProvider services)
        {

            // Apply Db Migrations
            using var context = services.GetRequiredService<CatalagoArandaDBContext>();
            context.Database.EnsureCreated();
        }
    }
}