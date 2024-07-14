using OnlineBookShop.Loans.Dto;

namespace OnlineBookShop.Loans.Repository.interfaces
{
    public interface ILoanRepository
    {
        Task<List<LoanResponse>> GetAllAsync();

        Task<LoanResponse> GetByIdAsync(int id);

    }
}
