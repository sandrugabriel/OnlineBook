using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Repository.interfaces;
using OnlineBookShop.Books.Services.interfaces;
using OnlineBookShop.Categories.Models;
using OnlineBookShop.System.Constants;
using OnlineBookShop.System.Exceptions;
using OnlineCarWash.System.Exceptions;
using OnlineCategoryShop.Categories.Repository.interfaces;

namespace OnlineBookShop.Books.Books
{
    public class BookCommandService : IBookCommandService
    {

        IBookRepository _repo;
        ICategoryRepository _repoCategory;

        public BookCommandService(IBookRepository repo, ICategoryRepository option)
        {
            _repo = repo;
            _repoCategory = option;
        }

        public async Task<BookResponse> CreateBook(CreateBookRequest createRequest)
        {
            if (createRequest.Title.Length <= 2) throw new InvalidName(Constants.InvalidName);

            if (createRequest.Author.Length <= 2) throw new InvalidName(Constants.InvalidName);

            if (createRequest.AvailableCopies <= 0) throw new InvalidCopy(Constants.InvalidCopy);

            var category = await _repoCategory.GetByName(createRequest.NameCategory);
            if (category == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);


            return await _repo.CreateBook(createRequest,category);
        }
       public async Task<BookResponse> UpdateBook(int id, UpdateBookRequest updateRequest)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            if (updateRequest.AvailableCopies <= 0) throw new InvalidCopy(Constants.InvalidCopy);

            Category category = null;
            if (updateRequest.NameCategory != null)
            {
               category = await _repoCategory.GetByName(updateRequest.NameCategory);
                if (category == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            book = await _repo.UpdateBook(id, updateRequest,category);

            return book;
        }

        public async Task<BookResponse> DeleteBook(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            await _repo.DeleteBook(id);

            return book;
        }

 
    }
}
