using Moq;
using OnlineBookShop.Loans.Dto;
using OnlineBookShop.Loans.Repository.interfaces;
using OnlineBookShop.Loans.Service;
using OnlineBookShop.Loans.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Loans.Helpers;

namespace Tests.Loans.UnitTests
{
    public class TestLoanQueryService
    {
        private readonly Mock<ILoanRepository> _mock;
        private readonly ILoanQueryService _queryServiceLoan;

        public TestLoanQueryService()
        {
            _mock = new Mock<ILoanRepository>();
            _queryServiceLoan = new LoanQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAllLoan_ReturnLoan()
        {
            var loans = TestLoanFactory.CreateLoans(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(loans);

            var result = await _queryServiceLoan.GetAllAsync();

            Assert.Equal(5, result.Count);

        }

        [Fact]
        public async Task GetByIdLoan_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((LoanResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryServiceLoan.GetByIdAsync(1));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByIdLoan_ReturnLoan()
        {
            var loan = TestLoanFactory.CreateLoan(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(loan);

            var result = await _queryServiceLoan.GetByIdAsync(1);

            Assert.Equal(1, result.CustomerId);

        }


    }
}
