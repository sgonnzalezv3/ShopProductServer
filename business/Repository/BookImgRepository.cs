using AutoMapper;
using business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business.Repository
{
    public class BookImgRepository : IBookImgRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookImgRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> AddBookImg(BookImgDto imgDto)
        {
            /* Convertirlo a DTO */
            var img = _mapper.Map<BookImgDto, BookImg>(imgDto);
            await _context.BookImg.AddAsync(img);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteBookImgByBookId(int bookId)
        {
            var findBookImg = await _context.BookImg.FindAsync(bookId);
            if(findBookImg != null)
                _context.BookImg.Remove(findBookImg);
            return 0;
        }

        public async Task<int> DeleteBookImgById(int imgId)
        {
            var findBookImgList = await _context.BookImg.Where(x=> x.BookId == imgId).ToListAsync();
            if (findBookImgList != null)
            {
                _context.BookImg.RemoveRange(findBookImgList);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteBookImgByUrl(string imgUrl)
        {
            /* Obtener todas las imagenes a eliminar */
            var images = await _context.BookImg.FirstOrDefaultAsync(x => x.BookImageUrl.ToLower() == imgUrl.ToLower());
            if(images == null)
            {
                return 0;
            }
            _context.BookImg.Remove(images);
            return await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<BookImgDto>> GetBookList(int bookId)
        {
            var bookImgList = await _context.BookImg.Where(x => x.BookId == bookId).ToListAsync();
            var bookImgListDto = _mapper.Map<IEnumerable<BookImg>,IEnumerable <BookImgDto>>(bookImgList);
            return bookImgListDto;
        }
    }
}
