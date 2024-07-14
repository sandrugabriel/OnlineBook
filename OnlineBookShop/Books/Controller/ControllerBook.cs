using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Categories.Controller.interfaces;
using OnlineBookShop.Categories.Dto;
using OnlineCarWash.System.Exceptions;
using OnlineBookShop.Books.Services.interfaces;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.System.Exceptions;

namespace OnlineBookShop.Categories.Controller
{
    public class ControllerBook : ControllerAPIBook
    {
        IBookQueryService _query;
        IBookCommandService _command;

        public ControllerBook(IBookQueryService query, IBookCommandService command)
        {
            _query = query;
            _command = command;
        }

        [Authorize]
        public override async Task<ActionResult<List<BookResponse>>> GetAll()
        {
            try
            {
                var books = await _query.GetAllAsync();
                return Ok(books);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<BookResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var books = await _query.GetByIdAsync(id);
                return Ok(books);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<BookResponse>> GetByTitle([FromQuery] string title)
        {
            try
            {
                var books = await _query.GetByTitleAsync(title);
                return Ok(books);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<BookResponse>> CreateBook([FromBody] CreateBookRequest createRequestBook)
        {
            try
            {
                var book = await _command.CreateBook(createRequestBook);
                return Ok(book);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidCopy ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        public override async Task<ActionResult<BookResponse>> UpdateBook([FromQuery] int id, [FromBody] UpdateBookRequest updateRequest)
        {
            try
            {
                var book = await _command.UpdateBook(id, updateRequest);
                return Ok(book);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        public override async Task<ActionResult<BookResponse>> DeleteBook([FromQuery] int id)
        {
            try
            {
                var book = await _command.DeleteBook(id);
                return Ok(book);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
