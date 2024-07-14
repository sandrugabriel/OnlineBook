using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Models;
using OnlineBookShop.Categories.Models;

namespace OnlineBookShop.Books.Repository.interfaces
{
    public interface IBookRepository
    {
        Task<List<BookResponse>> GetAllAsync();

        Task<BookResponse> GetByIdAsync(int id);

        Task<BookResponse> GetByTitleAsync(string name);

        Task<Book> GetById(int id);

        Task<Book> GetByTitle(string name);

        Task<BookResponse> CreateBook(CreateBookRequest createRequest, Category category);

        Task<BookResponse> UpdateBook(int id, UpdateBookRequest updateRequest, Category category);

        Task<BookResponse> DeleteBook(int id);



    }
}
