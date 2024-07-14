using OnlineBookShop.Loans.Dto;

namespace OnlineBookShop.Loans.Service.interfaces
{
    public interface ILoanQueryService
    {

        Task<List<LoanResponse>> GetAllAsync();

        Task<LoanResponse> GetByIdAsync(int id);

    }
}
