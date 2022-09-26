using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Productos.Apì.Interfaces
{
    public interface IGuardarArchivo
    {

        public Task<string> Crear(byte[] file, string contentType, string extension, string container, string nombre);
        public Task Borrar(string ruta, string container);

    }
}
