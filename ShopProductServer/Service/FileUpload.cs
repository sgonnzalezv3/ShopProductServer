using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopProductServer.Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProductServer.Service
{
    public class FileUpload : IFileUpload
    {
        /* esto nos permite hallar la ruta de la carpeta en wwwrooot 
         usamos el entorno de alojamiento web*/
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteFile(string fileName)
        {
            try
            {
                /* Obtenemos toda la ruta del archivo */
                var path = $"{_webHostEnvironment.WebRootPath}\\BookImages\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /* Cargar archivo dentro de wwwroot */
        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                /* Obtener Info del Archivo */
                FileInfo fileInfo = new FileInfo(file.Name);
                /* Nombre único con guid al archivo para guardarlo */
                var newUniqueFileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                /* ruta de la carpeta */
                var urlFile = $"{_webHostEnvironment.WebRootPath}\\BookImages";
                /* Ruta completa con el nombre del archivo */
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "BookImages", newUniqueFileName);
                /* Necesitamos mover el archivo, entonces creamos un flujo de memoria. */
                var memoryStream = new MemoryStream();
                /* Leer Archivo que viene del metodo y copiarlo al memorystream */
                await file.OpenReadStream().CopyToAsync(memoryStream);
                /* Verificar si no existe la carpeta donde va estar el archivo */
                if (!Directory.Exists(urlFile))
                {
                    Directory.CreateDirectory(urlFile);
                }
                /* Con el flujo de memoria, escribir ese archivo en la carpeta nueva */
                                                  /* Ruta         Creacion     Tipo de acceso(escritura)  */
                await using(var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    /*escribir archivo*/
                    memoryStream.WriteTo(fs);
                }

                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                /* Una vez movido el file, solo se necesita la ruta. */
                var fullMovedPath = $"{url}BookImages/{newUniqueFileName}";
                return fullMovedPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
