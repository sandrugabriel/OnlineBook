using Moq;
using OnlineBookShop.Books.Books;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Repository.interfaces;
using OnlineBookShop.Books.Services.interfaces;
using OnlineBookShop.Categories.Models;
using OnlineBookShop.System.Constants;
using OnlineBookShop.System.Exceptions;
using OnlineCarWash.System.Exceptions;
using OnlineCategoryShop.Categories.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Books.Helpers;

namespace Tests.Books.UnitTests
{
    public class TestBookCommandService
    {
        private readonly Mock<IBookRepository> _mock;
        private readonly Mock<ICategoryRepository> _mockCategory;

        private readonly IBookCommandService _commandBook;

        public TestBookCommandService()
        {
            _mock = new Mock<IBookRepository>();
            _mockCategory = new Mock<ICategoryRepository>();
            _commandBook = new BookCommandService(_mock.Object, _mockCategory.Object);

        }

        [Fact]
        public async Task CreateBook_InvalidAvailableCopies()
        {
            var createRequest = new CreateBookRequest
            {
                AvailableCopies = 0,
                Title = "adsda",
                NameCategory = "sad",
                Author = "asd"
            };

            var category = new Category();
            category.Name = "sad";
            category.Books = new List<OnlineBookShop.Books.Models.Book>();
            _mockCategory.Setup(repo => repo.GetByName("sad")).ReturnsAsync(category);

            _mock.Setup(repo => repo.CreateBook(createRequest, category)).ReturnsAsync((BookResponse)null);
            Exception exception = await Assert.ThrowsAsync<InvalidCopy>(() => _commandBook.CreateBook(createRequest));

            Assert.Equal(Constants.InvalidCopy, exception.Message);
        }

        [Fact]
        public async Task CreateBook_ReturnBook()
        {
            var createRequest = new CreateBookRequest
            {
                AvailableCopies = 10,
                Title = "adsda",
                NameCategory = "sad",
                Author = "asd"
            };

            var book = TestBookFactory.CreateBook(50);
            book.AvailableCopies = createRequest.AvailableCopies;

            var category = new Category
            {
                Name = "sad"
            };

            _mockCategory.Setup(repo => repo.GetByName("sad")).ReturnsAsync(category);
            _mock.Setup(repo => repo.CreateBook(It.IsAny<CreateBookRequest>(), category)).ReturnsAsync(book);

            Assert.NotNull(_commandBook);

            var result = await _commandBook.CreateBook(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.AvailableCopies, createRequest.AvailableCopies);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateBookRequest
            {
                AvailableCopies = 0,
                Title = "adsda"
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((BookResponse)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandBook.UpdateBook(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidAvailableCopies()
        {
            var updateRequest = new UpdateBookRequest
            {
                AvailableCopies = 0,
                Title = "adsda"
            };

            var book = TestBookFactory.CreateBook(1);
            book.AvailableCopies = updateRequest.AvailableCopies.Value;
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var exception = await Assert.ThrowsAsync<InvalidCopy>(() => _commandBook.UpdateBook(1, updateRequest));

            Assert.Equal(Constants.InvalidCopy, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData_ReturnBook()
        {
            var updateRequest = new UpdateBookRequest
            {
                Author = "asd",
                AvailableCopies = 10,
                Title = "adsda"
            };

            var book = TestBookFactory.CreateBook(1);


            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(book);
            _mock.Setup(repo => repo.UpdateBook(It.IsAny<int>(), It.IsAny<UpdateBookRequest>(),null)).ReturnsAsync(book);

            var result = await _commandBook.UpdateBook(1, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(book, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteBook(It.IsAny<int>())).ReturnsAsync((BookResponse)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandBook.DeleteBook(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var book = TestBookFactory.CreateBook(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(book);

            var restul = await _commandBook.DeleteBook(1);

            Assert.NotNull(restul);
            Assert.Equal(book, restul);
        }



    }
}
