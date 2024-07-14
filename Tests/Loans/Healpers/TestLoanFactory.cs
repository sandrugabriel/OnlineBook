
using OnlineBookShop.Loans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Loans.Helpers
{
    public class TestLoanFactory
    {
        public static LoanResponse CreateLoan(int id)
        {
            return new LoanResponse
            {
                Id = id,
                BookId =  id,
                BorrowDate = DateTime.Now,
                CustomerId = id,
                ReturnDate = DateTime.Now.AddDays(7),
            };
        }

        public static List<LoanResponse> CreateLoans(int count)
        {

            List<LoanResponse> responses = new List<LoanResponse>();

            for (int i = 0; i < count; i++)
            {
                responses.Add(CreateLoan(i));
            }

            return responses;
        }
    }
}
