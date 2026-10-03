using AutoMapper;
using BookCatalogApi.DTOs;
using BookCatalogApi.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookCatalogApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}