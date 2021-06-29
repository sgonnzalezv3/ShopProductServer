using AutoMapper;
using DataAccess.Data;
using Models;

namespace business.Mapper
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto,Book>().ReverseMap();
            CreateMap<BookImgDto,BookImg>().ReverseMap();
        }
    }
}
