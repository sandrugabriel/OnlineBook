using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Categories.Dto;

namespace OnlineBookShop.Categories.Controller.interfaces
{

    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIBook : ControllerBase
    {


        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<BookResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<BookResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(BookResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<BookResponse>> GetById([FromQuery] int id);

        [HttpGet("FindByTitle")]
        [ProducesResponseType(statusCode: 200, type: typeof(BookResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<BookResponse>> GetByTitle([FromQuery] string title);

        [HttpPost("CreateBook")]
        [ProducesResponseType(statusCode: 201, type: typeof(BookResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<BookResponse>> CreateBook([FromBody] CreateBookRequest createRequestBook);

        [HttpPut("UpdateBook")]
        [ProducesResponseType(statusCode: 200, type: typeof(BookResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<BookResponse>> UpdateBook([FromQuery] int id, [FromBody] UpdateBookRequest updateRequest);

        [HttpDelete("DeleteBook")]
        [ProducesResponseType(statusCode: 200, type: typeof(BookResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<BookResponse>> DeleteBook([FromQuery] int id);

    }
}
