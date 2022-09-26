using Aranda.Productos.BusinessLogic.EntitiesDomain;
using Aranda.Productos.BusinessLogic.Exceptions;
using Aranda.Productos.BusinessLogic.Interfaces;
using Aranda.Productos.Persistence.Entities;
using Aranda.Productos.Persistence.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.BusinessLogic.Logic
{
    public class ProductoLogic: IProductoLogic
    {        
        private readonly IMapper _mapper;
        private readonly IProductoRepositorio _productoRepository;

        public ProductoLogic(IMapper mapper, IProductoRepositorio productoRepository)
        {
            _mapper = mapper;
            _productoRepository = productoRepository;
        }

                
        public async Task<IEnumerable<ProductoDomain>> GetAllAsync(int pagina = 1, int pageSize = 2) => (await _productoRepository.GetAllAsync(pagina, pageSize)).Select( x => _mapper.Map<ProductoDomain>(x));

        public async Task<IEnumerable<ProductoDomain>> GetAllByAsync(string buscar, string ordenarPor, int pagina = 1, int pageSize = 2) => (await _productoRepository.GetAllByAsync(buscar, ordenarPor, pagina, pageSize)).Select(x => _mapper.Map<ProductoDomain>(x));

        public async Task<ProductoDomain> GetAsync(int id)
        {
            try
            {
                return _mapper.Map<ProductoDomain>(await _productoRepository.GetAsync(id));
            }
            catch { throw new ApiException("No Existe el Producto"); }            
            
        }

        public async Task<ProductoDomain> AddAsync(ProductoDomain productoDomain)
        {
            if (productoDomain == null) throw new ApiException("Falta Información del Producto");
            return _mapper.Map<ProductoDomain>(await _productoRepository.AddAsync(_mapper.Map<Producto>(productoDomain)));
        }

        public async Task DeleteAsync(int id) => await _productoRepository.DeleteAsync(id);

        public async Task<bool> UpdateAsync(ProductoDomain productoDomain)
        {
            if (productoDomain == null) throw new ApiException("Falta Información del Producto");
            return await _productoRepository.UpdateAsync(_mapper.Map<Producto>(productoDomain));
        }

    }
}
