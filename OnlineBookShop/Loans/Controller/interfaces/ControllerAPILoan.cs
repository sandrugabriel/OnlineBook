using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Loans.Dto;

namespace OnlineBookShop.Loans.Controller.interfaces
{

    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPILoan : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<LoanResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<LoanResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(LoanResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<LoanResponse>> GetById([FromQuery] int id);


    }
}
