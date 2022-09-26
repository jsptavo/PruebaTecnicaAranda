using Aranda.Productos.Persistence.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.BusinessLogic.Dependency
{
    public class ApplyDbMigrationsLogic
    {
        public static void Apply(IServiceProvider services)
        {
            ApplyDbMigrations.Apply(services);
        }
    }
}
