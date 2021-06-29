using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProductServer.Service.IService
{
    public interface IFileUpload
    {
        /* Subida de archivos */
        Task<string> UploadFile(IBrowserFile file);

        /* Eliminar archivo */
        bool DeleteFile(string fileName);
    }
}
