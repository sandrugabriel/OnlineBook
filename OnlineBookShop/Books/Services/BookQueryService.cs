using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Repository.interfaces;
using OnlineBookShop.Books.Services.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;

namespace OnlineBookShop.Books.Books
{
    public class BookQueryService : IBookQueryService
    {

        IBookRepository _repo;

        public BookQueryService(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<BookResponse>> GetAllAsync()
        {
            var book = await _repo.GetAllAsync();
            if (book.Count == 0) return new List<BookResponse>();
            return book.ToList();
        }

        public async Task<BookResponse> GetByIdAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            return book;
        }

        public async Task<BookResponse> GetByTitleAsync(string name)
        {
            var book = await _repo.GetByTitleAsync(name);
            if (book == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            return book;
        }

    }
}
