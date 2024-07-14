using Moq;
using OnlineBookShop.Books.Books;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Repository.interfaces;
using OnlineBookShop.Books.Services.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Books.Helpers;

namespace Tests.Books.UnitTests
{
    public class TestBookQueryService
    {
        private readonly Mock<IBookRepository> _mock;
        private readonly IBookQueryService _queryBookBook;

        public TestBookQueryService()
        {
            _mock = new Mock<IBookRepository>();
            _queryBookBook = new BookQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAllBook_ReturnBook()
        {
            var books = TestBookFactory.CreateBooks(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            var result = await _queryBookBook.GetAllAsync();

            Assert.Equal(5, result.Count);

        }

        [Fact]
        public async Task GetByIdBook_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((BookResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryBookBook.GetByIdAsync(1));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByIdBook_ReturnBook()
        {
            var book = TestBookFactory.CreateBook(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var result = await _queryBookBook.GetByIdAsync(1);

            Assert.Equal("test1", result.Title);

        }


        [Fact]
        public async Task GetByTitleBook_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByTitleAsync("test")).ReturnsAsync((BookResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryBookBook.GetByTitleAsync("test"));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByTitleBook_ReturnBook()
        {
            var book = TestBookFactory.CreateBook(1);
            _mock.Setup(repo => repo.GetByTitleAsync("test1")).ReturnsAsync(book);

            var result = await _queryBookBook.GetByTitleAsync("test1");

            Assert.Equal("test1", result.Title);

        }
    }
}
