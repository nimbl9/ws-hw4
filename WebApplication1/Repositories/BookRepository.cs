using BookCatalogApi.Data;
using BookCatalogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Book>> GetByAuthorAsync(string author)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(x => x.Author.Contains(author))
                .ToListAsync();
        }

        public async Task<Book> CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);

            if (existingBook == null)
                return false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            existingBook.Price = book.Price;
            existingBook.Description = book.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}