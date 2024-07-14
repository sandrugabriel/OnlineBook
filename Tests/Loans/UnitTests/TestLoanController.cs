using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineBookShop.Loans.Controller;
using OnlineBookShop.Loans.Controller.interfaces;
using OnlineBookShop.Loans.Dto;
using OnlineBookShop.Loans.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Loans.Helpers;
namespace Tests.Loans.UnitTests
{
    public class TestLoanController
    {
        private readonly Mock<ILoanQueryService> _mockQueryService;
        private readonly ControllerAPILoan loanApiController;

        public TestLoanController()
        {
            _mockQueryService = new Mock<ILoanQueryService>();

            loanApiController = new ControllerLoan(_mockQueryService.Object);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var loans = TestLoanFactory.CreateLoans(5);
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ReturnsAsync(loans);

            var result = await loanApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allLoans = Assert.IsType<List<LoanResponse>>(okResult.Value);

            Assert.Equal(5, allLoans.Count);
            Assert.Equal(200, okResult.StatusCode);

        }


        [Fact]
        public async Task GetById_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetByIdAsync(10)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await loanApiController.GetById(10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var custoemrs = TestLoanFactory.CreateLoan(1);
            _mockQueryService.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(custoemrs);

            var result = await loanApiController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allLoans = Assert.IsType<LoanResponse>(okResult.Value);

            Assert.Equal(1, allLoans.CustomerId);
            Assert.Equal(200, okResult.StatusCode);

        }

    }
}
