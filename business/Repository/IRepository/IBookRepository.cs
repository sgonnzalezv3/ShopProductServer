using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business.Repository.IRepository
{
    /* Interface que contentiene las múltiples acciones posibles para realizar con libros. */
    public interface IBookRepository
    {
        public Task<BookDto> AddBook(BookDto bookDto);
        public Task<BookDto> UpdateBook(int bookId,BookDto bookDto);
        public Task<BookDto> GetBook(int bookId);
        public Task<IEnumerable<BookDto>> GetBookList();
        /* Se pone por defecto = 0 porque puede que no se utilice  */
        public Task<BookDto> BookNameExists(string name, int bookId = 0);
        public Task<int> DeleteBook(int bookId);

    }
}
