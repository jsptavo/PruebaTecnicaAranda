using Aranda.Productos.BusinessLogic.EntitiesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.BusinessLogic.Interfaces
{
    public interface IProductoLogic
    {
        public Task<IEnumerable<ProductoDomain>> GetAllAsync(int pagina = 1, int pageSize = 2);
        public Task<IEnumerable<ProductoDomain>> GetAllByAsync(string buscar, string ordenarPor, int pagina = 1, int pageSize = 2);
        public Task<ProductoDomain> GetAsync(int id);
        public Task<ProductoDomain> AddAsync(ProductoDomain productoDomain);        
        public Task DeleteAsync(int id);

        public Task<bool> UpdateAsync(ProductoDomain productoDomain);

    }
}
