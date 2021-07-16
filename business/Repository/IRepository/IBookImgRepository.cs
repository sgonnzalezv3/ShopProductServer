using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business.Repository.IRepository
{
    public interface IBookImgRepository
    {
        /* no queremos devolver la img, sino un valor(int) definiendo si fue exitoso(1) o no (0) */
        public Task<int> AddBookImg(BookImgDto imgDto);
        /* Eliminar una imagen según su ID */
        public Task<int> DeleteBookImgById(int imgId);
        /* Eliminar todas las imagenes de un libro segun libroId*/
        public Task<int> DeleteBookImgByBookId(int bookId);
        /* Eliminar libro Imagen por URL */
        public Task<int> DeleteBookImgByUrl(string imgUrl);
        /* obtener todas las imagenes de un libro */
        public Task<IEnumerable<BookImgDto>> GetBookList(int bookId);

    }
}
