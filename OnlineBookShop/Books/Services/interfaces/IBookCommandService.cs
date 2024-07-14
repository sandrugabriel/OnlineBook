using OnlineBookShop.Books.Dto;

namespace OnlineBookShop.Books.Services.interfaces
{
    public interface IBookCommandService
    {
        Task<BookResponse> CreateBook(CreateBookRequest createRequest);

        Task<BookResponse> UpdateBook(int id, UpdateBookRequest updateRequest);

        Task<BookResponse> DeleteBook(int id);
    }
}
