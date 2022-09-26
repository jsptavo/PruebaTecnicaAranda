using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Productos.Api.Dto
{
    public class ProductoAddImagenDTO
    {
        //public int ProductoID { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Categoria es obligatorio")]
        [StringLength(100)]
        public string Categoria { get; set; }
        public IFormFile Imagen { get; set; }

    }
}
