using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Services.interfaces;
using OnlineBookShop.Categories.Controller;
using OnlineBookShop.Categories.Controller.interfaces;
using OnlineBookShop.System.Constants;
using OnlineBookShop.System.Exceptions;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Books.Helpers;

namespace Tests.Books.UnitTests
{
    public class TestControllerBook
    {
        private readonly Mock<IBookCommandService> _mockCommandBook;
        private readonly Mock<IBookQueryService> _mockQueryBook;
        private readonly ControllerAPIBook bookApiController;

        public TestControllerBook()
        {
            _mockCommandBook = new Mock<IBookCommandService>();
            _mockQueryBook = new Mock<IBookQueryService>();

            bookApiController = new ControllerBook(_mockQueryBook.Object, _mockCommandBook.Object);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var books = TestBookFactory.CreateBooks(5);
            _mockQueryBook.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            var result = await bookApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allBooks = Assert.IsType<List<BookResponse>>(okResult.Value);

            Assert.Equal(5, allBooks.Count);
            Assert.Equal(200, okResult.StatusCode);

        }


        [Fact]
        public async Task GetById_ItemsDoNotExist()
        {
            _mockQueryBook.Setup(repo => repo.GetByIdAsync(10)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await bookApiController.GetById(10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var books = TestBookFactory.CreateBook(1);
            _mockQueryBook.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(books);

            var result = await bookApiController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allBooks = Assert.IsType<BookResponse>(okResult.Value);

            Assert.Equal("test1", allBooks.Title);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task GetByAvailableCopies_ItemsDoNotExist()
        {
            _mockQueryBook.Setup(repo => repo.GetByTitleAsync("10")).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await bookApiController.GetByTitle("10");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetByAvailableCopies_ValidData()
        {
            var books = TestBookFactory.CreateBook(1);
            _mockQueryBook.Setup(repo => repo.GetByTitleAsync("test1")).ReturnsAsync(books);

            var result = await bookApiController.GetByTitle("test1");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allBooks = Assert.IsType<BookResponse>(okResult.Value);

            Assert.Equal("test1", allBooks.Title);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidAvailableCopies()
        {

            var createRequest = new CreateBookRequest
            {

                Title = "test",
                AvailableCopies = -1
            };


            _mockCommandBook.Setup(repo => repo.CreateBook(It.IsAny<CreateBookRequest>())).
                ThrowsAsync(new InvalidCopy(Constants.InvalidCopy));

            var result = await bookApiController.CreateBook(createRequest);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal(Constants.InvalidCopy, badRequest.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateBookRequest
            {
                Title = "test",
                AvailableCopies = 10
            };

            var book = TestBookFactory.CreateBook(1);
            book.AvailableCopies = createRequest.AvailableCopies;

            _mockCommandBook.Setup(repo => repo.CreateBook(It.IsAny<CreateBookRequest>())).ReturnsAsync(book);

            var result = await bookApiController.CreateBook(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, book);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateBookRequest
            {
                Title = "test",
                AvailableCopies = 0
            };


            _mockCommandBook.Setup(repo => repo.UpdateBook(1, updateRequest)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await bookApiController.UpdateBook(1, updateRequest);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
        {
            var updateRequest = new UpdateBookRequest
            {
                Title = "test",
                AvailableCopies = 10
            };

            var book = TestBookFactory.CreateBook(1);

            _mockCommandBook.Setup(repo => repo.UpdateBook(It.IsAny<int>(), It.IsAny<UpdateBookRequest>())).ReturnsAsync(book);

            var result = await bookApiController.UpdateBook(1, updateRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, book);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandBook.Setup(repo => repo.DeleteBook(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await bookApiController.DeleteBook(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var book = TestBookFactory.CreateBook(1);

            _mockCommandBook.Setup(repo => repo.DeleteBook(It.IsAny<int>())).ReturnsAsync(book);

            var result = await bookApiController.DeleteBook(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, book);

        }


    }
}
