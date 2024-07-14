using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Loans.Controller.interfaces;
using OnlineBookShop.Loans.Dto;
using OnlineBookShop.Loans.Service.interfaces;
using OnlineCarWash.System.Exceptions;

namespace OnlineBookShop.Loans.Controller
{
    public class ControllerLoan : ControllerAPILoan
    {

        ILoanQueryService _query;

        public ControllerLoan(ILoanQueryService query)
        {
            _query = query;
        }

        [Authorize]
        public override async Task<ActionResult<List<LoanResponse>>> GetAll()
        {
            try
            {
                var loans = await _query.GetAllAsync();
                return Ok(loans);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<LoanResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var loans = await _query.GetByIdAsync(id);
                return Ok(loans);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
