using AutoMapper;
using business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace business.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<BookDto> AddBook(BookDto bookDto)
        {
            Book book = _mapper.Map<BookDto, Book>(bookDto);
            book.CreationDate = DateTime.Now;
            var addBook = await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            /* .Entity asegura para darnos el objeto book para luego convertirlo a un DTO y despues retornarlo */
            return _mapper.Map<Book, BookDto>(addBook.Entity);
        }

        public async Task<BookDto> BookNameExists(string name, int bookId = 0 )
        {
            try
            {
                if(bookId == 0)
                {
                    var book = _mapper.Map<Book, BookDto>(await _context.Book.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                    return book;
                }
                else
                {
                    /* algo parecido, pero verificamos que el bookid realmente sea diferente, para realmente dejar claro que hay más de un libro con ese nombre
                     * si el id es el mismo, entonces no arrojara errores y seguirá el proceso.
                     */
                    var book = _mapper.Map<Book, BookDto>(await _context.Book.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != bookId));
                    return book;
                }

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<int> DeleteBook(int bookId)
        {
            var getBook = await _context.Book.FindAsync(bookId);
            if(getBook != null)
            {
                _context.Book.Remove(getBook);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<BookDto> GetBook(int bookId)
        {
            try
            {
                var getBook = await _context.Book.FindAsync(bookId);
                var getBookDto = _mapper.Map<Book,BookDto>(getBook);
                return getBookDto;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<BookDto>> GetBookList()
        {
            try
            {
                var bookList = await _context.Book.ToListAsync();
                var list = (IEnumerable<Book>)bookList;
                var listDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(list);
                //IEnumerable<BookDto> bookList = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(_context.Book);
                return listDto;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<BookDto> UpdateBook(int bookId, BookDto bookDto)
        {
            try
            {
                if (bookId == bookDto.Id)
                {
                    var bookDetails = await _context.Book.FindAsync(bookId);
                                                       /* Pasar de la fuente | Al Destino */
                    var bookMapDto = _mapper.Map<BookDto, Book>(bookDto, bookDetails);
                    bookMapDto.UpdateDate= DateTime.Now;
                    var updateBook = _context.Book.Update(bookMapDto);
                    await _context.SaveChangesAsync();
                    /* .Entity asegura para darnos el objeto book para luego convertirlo a un DTO y despues retornarlo */
                    return _mapper.Map<Book, BookDto>(updateBook.Entity);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {

                return null;
            }
        }
    }
}
