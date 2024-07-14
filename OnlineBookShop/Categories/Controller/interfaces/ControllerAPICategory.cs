using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Categories.Dto;

namespace OnlineBookShop.Categories.Controller.interfaces
{

    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPICategory : ControllerBase
    {


        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<CategoryResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<CategoryResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(CategoryResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<CategoryResponse>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(CategoryResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<CategoryResponse>> GetByName([FromQuery] string name);

        [HttpPost("CreateCategory")]
        [ProducesResponseType(statusCode: 201, type: typeof(CategoryResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<CategoryResponse>> CreateCategory([FromQuery] string name);

        [HttpPut("UpdateCategory")]
        [ProducesResponseType(statusCode: 200, type: typeof(CategoryResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CategoryResponse>> UpdateCategory([FromQuery] int id, [FromQuery] string name);

        [HttpDelete("DeleteCategory")]
        [ProducesResponseType(statusCode: 200, type: typeof(CategoryResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CategoryResponse>> DeleteCategory([FromQuery] int id);

    }
}
