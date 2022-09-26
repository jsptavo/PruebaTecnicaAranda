using Aranda.Productos.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.Persistence.Interfaces
{
    public interface IProductoRepositorio
    {      

        public Task<IEnumerable<Producto>> GetAllAsync(int pagina = 1, int pageSize = 2);
        public Task<IEnumerable<Producto>> GetAllByAsync(string buscar, string ordenarPor, int pagina = 1, int pageSize = 2);
        public Task<Producto> GetAsync(int id);        
        public Task<Producto> AddAsync(Producto producto);
        public Task DeleteAsync(int id);
        public Task<bool> UpdateAsync(Producto producto);

    }
}
