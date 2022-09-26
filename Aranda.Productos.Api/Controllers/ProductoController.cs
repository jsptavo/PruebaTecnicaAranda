using Aranda.Productos.Api.Dto;
using Aranda.Productos.Apì.Interfaces;
using Aranda.Productos.BusinessLogic.EntitiesDomain;
using Aranda.Productos.BusinessLogic.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Productos.Api.Controllers
{   
    public class ProductoController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IProductoLogic _productoLogic;
        private readonly IGuardarArchivo _guardarArchivo;



        /// <summary>
        /// Initializes a new instance of the <see cref="ProductoController"/> class.
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="usdExchangeRateLogic">The resource USD logic</param>
        /// <param name="brlExchangeRateLogic">The resource BRL repository</param>
        public ProductoController(IMapper mapper, ILogger<ProductoController> logger, IProductoLogic productoLogic, IGuardarArchivo guardarArchivo) : base(logger)
        {
            _mapper = mapper;
            _productoLogic = productoLogic;
            _guardarArchivo = guardarArchivo;
        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <remarks>
        /// 
        ///     GET /
        /// 
        /// </remarks>
        /// <returns></returns>
        //[SwaggerOperation(OperationId = "GetAll", Summary = "Obtener todos los productos")]
        //[SwaggerResponse(200, "Return all products", typeof(IEnumeble<ProductoDTO>))]
        //[SwaggerResponse(500, "Return an internal error", typeof(string))]        
        [HttpGet]
        [Route("GetAll/{pagina}/{paginaTamano}")]
        public async Task<IActionResult> List([FromRoute] int pagina = 1, [FromRoute] int paginaTamano = 2)
        {
            var result = (await _productoLogic.GetAllAsync(pagina, paginaTamano)).Select(x => _mapper.Map<ProductoDTO>(x));
            return Ok(result);
        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <remarks>
        /// 
        ///     GET /
        /// 
        /// </remarks>
        /// <returns></returns>
        //[SwaggerOperation(OperationId = "GetAll", Summary = "Buscar los productos por Nombre, Descripcion o Categoria")]
        //[SwaggerResponse(200, "Return all products", typeof(IEnumeble<ProductoDTO>))]
        //[SwaggerResponse(500, "Return an internal error", typeof(string))]        
        [HttpGet]
        [Route("GetAllBy/{buscar}/{ordenarPor}/{pagina}/{paginaTamano}")]        
        public async Task<IActionResult> ListBy([FromRoute] string buscar, [FromRoute] string ordenarPor = "nombre_asc", [FromRoute] int pagina = 1, [FromRoute] int paginaTamano = 2)
        {
            var result = (await _productoLogic.GetAllByAsync(buscar.ToLower(), ordenarPor.ToLower(), pagina, paginaTamano)).Select(x => _mapper.Map<ProductoDTO>(x));
            return Ok(result);
        }


        /// <summary>
        /// Get product by id
        /// </summary>
        /// <remarks>
        /// 
        ///     GET /
        /// 
        /// </remarks>
        /// <returns></returns>        
        //[SwaggerOperation(OperationId = "Get", Summary = "Obtener producto por ID")]
        //[SwaggerResponse(200, "Return a product", typeof(ProductoDTO))]
        //[SwaggerResponse(500, "Return an internal error", typeof(string))]        
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
           
                var result = _mapper.Map<ProductoDTO>(await _productoLogic.GetAsync(id));
                return Ok(result);            
        }


        /// <summary>
        /// Add Product
        /// </summary>
        /// <remarks>
        /// 
        ///     POST /
        /// 
        /// </remarks>
        /// <returns></returns>
        //[SwaggerOperation(OperationId = "Add", Summary = "Agregar Producto")]
        //[SwaggerResponse(200, "Return producto created", typeof(ProductoDTO))]
        //[SwaggerResponse(500, "Return an internal error", typeof(string))]        
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromForm] ProductoAddImagenDTO productoAddImagenDTO)
        {          

                var prod = _mapper.Map<ProductoDomain>(productoAddImagenDTO);

                if (productoAddImagenDTO.Imagen != null)
                {
                    string imagenUrl = await GuardarImagen(productoAddImagenDTO.Imagen);
                    prod.Imagen = imagenUrl;
                }

                var result = _mapper.Map<ProductoDTO>(await _productoLogic.AddAsync(prod));
                return Created($"/{result.ProductoID}", result);            
        }



        /// <summary>
        /// Actualizar Producto
        /// </summary>
        /// <remarks>
        /// 
        ///     PUT /
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put(int productoID, [FromForm] ProductoAddImagenDTO productoAddImagenDTO)
        {           
                var prodGuardado = await _productoLogic.GetAsync(productoID);
                
                var prod = _mapper.Map<ProductoDomain>(productoAddImagenDTO);
                    prod.ProductoID = productoID;

            if (productoAddImagenDTO.Imagen != null)
            {
                if (!string.IsNullOrEmpty(prodGuardado.Imagen))
                {
                    await _guardarArchivo.Borrar(prodGuardado.Imagen, "ImagenProducto");
                }

                string imagenUrl = await GuardarImagen(productoAddImagenDTO.Imagen);
                prod.Imagen = imagenUrl;
            }
         
                var result = await _productoLogic.UpdateAsync(prod);
                return Ok(result);          
        }


        /// <summary>
        /// Eliminar Producto
        /// </summary>
        /// <remarks>
        /// 
        ///     DELETE /
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
                var prodGuardado = await _productoLogic.GetAsync(id);               

                if (!string.IsNullOrEmpty(prodGuardado.Imagen))
                {
                    await _guardarArchivo.Borrar(prodGuardado.Imagen, "ImagenProducto");
                }

                await _productoLogic.DeleteAsync(id);
                return NoContent();          

        }


        private async Task<string> GuardarImagen(IFormFile imagen) {

           using var stream = new MemoryStream();
           
           await imagen.CopyToAsync(stream);

           var fileBytes = stream.ToArray();

            return await _guardarArchivo.Crear(fileBytes, imagen.ContentType, Path.GetExtension(imagen.FileName), "ImagenProducto", Guid.NewGuid().ToString());
        }



       


    }
}
