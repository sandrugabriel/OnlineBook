using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Data;
using OnlineBookShop.Loans.Dto;
using OnlineBookShop.Loans.Repository.interfaces;
namespace OnlineBookShop.Loans.Repository
{
    public class LoanRepository : ILoanRepository
    {
        AppDbContext _context;
        IMapper _mapper;

        public LoanRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LoanResponse>> GetAllAsync()
        {
            var loans = await _context.Loans.Include(s => s.Customer).Include(s => s.Book).ToListAsync();
            return _mapper.Map<List<LoanResponse>>(loans);
        }

        public async Task<LoanResponse> GetByIdAsync(int id)
        {
            var loan = await _context.Loans.Include(s => s.Customer).Include(s => s.Book).FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<LoanResponse>(loan);
        }
    }
}
