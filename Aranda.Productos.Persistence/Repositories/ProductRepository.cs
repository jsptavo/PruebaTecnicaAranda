using Aranda.Productos.Persistence.Data;
using Aranda.Productos.Persistence.Entities;
using Aranda.Productos.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.Persistence.Repositories
{
    public class ProductoRepositorio : BaseRepositorio, IProductoRepositorio
    {
        public ProductoRepositorio(CatalagoArandaDBContext dbContext) : base(dbContext) { }
        
        public async Task<Producto> AddAsync(Producto producto)
        {

            await _dbContext
                .Productos
                .AddAsync(producto)
                .ConfigureAwait(false);

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            return producto;
        }

        public async Task DeleteAsync(int id)
        {
            var prod = await GetAsync(id);

            if (prod == null) throw new ArgumentNullException("Producto no existe");

            _dbContext.Productos.Remove(prod);

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Producto>> GetAllAsync(int pagina = 1, int pageSize = 2)
        {
            var listProductos = _dbContext.Productos.AsQueryable().OrderBy(p => p.Nombre);

            //Paginamos
            var result = PaginatedList<Producto>.Create(listProductos, pagina, pageSize);

            return result.Select(r => new Producto
            {
                ProductoID = r.ProductoID,
                Nombre = r.Nombre,
                Descripcion = r.Categoria,
                Imagen = r.Imagen
            }).ToList();

        }

        public async Task<IEnumerable<Producto>> GetAllByAsync(string buscar, string ordenarPor, int pagina = 1, int pageSize = 2)
        {
            var listProductos = _dbContext.Productos.AsQueryable();

            //Filtro
            if (!string.IsNullOrEmpty(buscar))
            {
                listProductos = listProductos.Where(p => p.Nombre.ToLower().Contains(buscar) ||
                                                    p.Descripcion.ToLower().Contains(buscar) ||
                                                    p.Categoria.ToLower().Contains(buscar));
            }

            //Ordenamos
            if (!String.IsNullOrEmpty(ordenarPor))
            {
                switch (ordenarPor)
                {

                    case "nombre_desc": listProductos = listProductos.OrderByDescending(p => p.Nombre); break;

                    case "categoria_asc": listProductos = listProductos.OrderBy(p => p.Categoria); break;

                    case "categoria_desc": listProductos = listProductos.OrderByDescending(p => p.Categoria); break;

                    default: listProductos = listProductos.OrderBy(p => p.Nombre); break;

                }
            }
            else
                listProductos = listProductos.OrderBy(p => p.Nombre);

            //Paginamos
            var result = PaginatedList<Producto>.Create(listProductos, pagina, pageSize);
                                    
            return result.Select(r => new Producto
                                {
                                    ProductoID = r.ProductoID,
                                    Nombre = r.Nombre,
                                    Descripcion = r.Categoria,
                                    Imagen = r.Imagen
                                }).ToList();
                        

        }

        public async Task<Producto> GetAsync(int id)
        {
            return await _dbContext
                .Productos
                .Where(x => x.ProductoID == id)
                .FirstAsync()
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(Producto producto)
        {
            var prod = await GetAsync(producto.ProductoID);

            if (prod == null) return false;

            prod.Nombre = producto.Nombre;
            prod.Descripcion = producto.Descripcion;
            prod.Categoria = producto.Categoria;
            prod.Imagen = producto.Imagen;
                        
            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            return true;
        }
    }
}
