using System;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Productos.Api.Dto
{
    public class ProductoDTO
    {
        public ProductoDTO() { }
               
        
        public int ProductoID { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Categoria es obligatorio")]
        [StringLength(100)]
        public string Categoria { get; set; }
                
        public string Imagen { get; set; }

    }
}
