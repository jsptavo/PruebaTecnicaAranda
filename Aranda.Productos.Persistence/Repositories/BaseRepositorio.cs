using Aranda.Productos.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Aranda.Productos.Persistence.Repositories
{
    public abstract class BaseRepositorio
    {
        protected readonly CatalagoArandaDBContext _dbContext;        
        public BaseRepositorio(CatalagoArandaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
