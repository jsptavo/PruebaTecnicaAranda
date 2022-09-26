
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Aranda.Productos.Apì.Interfaces;

namespace Aranda.Productos.Api.Utilities
{
    public class GuardarArchivo : IGuardarArchivo
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GuardarArchivo(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task Borrar(string ruta, string container)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;

            if (String.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }

            var nombreArchivo = Path.GetFileName(ruta);

            string pathFinal = Path.Combine(wwwrootPath, container, nombreArchivo);

            if (File.Exists(pathFinal))
            {
                File.Delete(pathFinal);
            }

            return Task.CompletedTask;

        }

        public async Task<string> Crear(byte[] file, string contentType, string extension, string container, string nombre)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;

            if (String.IsNullOrEmpty(wwwrootPath)) {
                throw new Exception();            
            }

            string capetaArchivo = Path.Combine(wwwrootPath, container);
            if (!Directory.Exists(capetaArchivo)) {

                Directory.CreateDirectory(capetaArchivo);
            }

            string nombreFinal = $"{nombre}{extension}";

            string rutaFinal = Path.Combine(capetaArchivo, nombreFinal);

            await File.WriteAllBytesAsync(rutaFinal, file);

            string urlActual = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            string dbUrl = Path.Combine(urlActual, container, nombreFinal).Replace("\\", "/");

            return dbUrl;

        }
    }
}
