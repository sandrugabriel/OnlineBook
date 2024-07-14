using OnlineBookShop.Loans.Dto;
using OnlineBookShop.Loans.Repository.interfaces;
using OnlineBookShop.Loans.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;

namespace OnlineBookShop.Loans.Service
{
    public class LoanQueryService : ILoanQueryService
    {

        ILoanRepository _repo;

        public LoanQueryService(ILoanRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<LoanResponse>> GetAllAsync()
        {
            var loans = await _repo.GetAllAsync();
            if (loans.Count == 0) return new List<LoanResponse>();

            return loans;
        }

        public async Task<LoanResponse> GetByIdAsync(int id)
        {
            var loan = await _repo.GetByIdAsync(id);
            if (loan == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return loan;
        }

    }
}
