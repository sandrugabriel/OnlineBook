using OnlineBookShop.Books.Dto;

namespace OnlineBookShop.Books.Services.interfaces
{
    public interface IBookQueryService
    {
        Task<List<BookResponse>> GetAllAsync();

        Task<BookResponse> GetByIdAsync(int id);

        Task<BookResponse> GetByTitleAsync(string name);
    }
}
