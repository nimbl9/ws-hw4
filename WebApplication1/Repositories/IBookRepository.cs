using BookCatalogApi.Entities;

namespace BookCatalogApi.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<List<Book>> GetByAuthorAsync(string author);
        Task<Book> CreateAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}