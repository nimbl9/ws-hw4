using AutoMapper;
using BookCatalogApi.DTOs;
using BookCatalogApi.Entities;
using BookCatalogApi.Models;
using BookCatalogApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BooksController(
            IBookRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ReturnResult<List<BookDto>>>> GetAll()
        {
            var books = await _repository.GetAllAsync();
            var result = _mapper.Map<List<BookDto>>(books);

            return Ok(ReturnResult<List<BookDto>>.Ok(
                result,
                "список книг получен"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnResult<BookDto>>> GetById(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return NotFound(ReturnResult<BookDto>.Fail("книга не найдена"));

            var result = _mapper.Map<BookDto>(book);

            return Ok(ReturnResult<BookDto>.Ok(
                result,
                "книга найдена"));
        }

        [HttpGet("author/{author}")]
        public async Task<ActionResult<ReturnResult<List<BookDto>>>> GetByAuthor(string author)
        {
            var books = await _repository.GetByAuthorAsync(author);
            var result = _mapper.Map<List<BookDto>>(books);

            return Ok(ReturnResult<List<BookDto>>.Ok(
                result,
                "поиск по автору выполнен"));
        }

        [HttpPost]
        public async Task<ActionResult<ReturnResult<BookDto>>> Create(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);

            var createdBook = await _repository.CreateAsync(book);

            var result = _mapper.Map<BookDto>(createdBook);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ReturnResult<BookDto>.Ok(
                    result,
                    "книга успешно добавлена"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReturnResult<BookDto>>> Update(
            int id,
            UpdateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            book.Id = id;

            var updated = await _repository.UpdateAsync(book);

            if (!updated)
                return NotFound(ReturnResult<BookDto>.Fail("книга не найдена"));

            var result = _mapper.Map<BookDto>(book);

            return Ok(ReturnResult<BookDto>.Ok(
                result,
                "книга успешно изменена"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReturnResult<object>>> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                return NotFound(ReturnResult<object>.Fail("книга не найдена"));

            return Ok(ReturnResult<object>.Ok(
                null!,
                "книга успешно удалена"));
        }
    }
}