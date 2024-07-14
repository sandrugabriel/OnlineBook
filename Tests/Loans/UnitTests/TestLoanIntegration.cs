using Newtonsoft.Json;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Categories.Dto;
using OnlineBookShop.Customers.Dto;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Tests.Infrastucture;
using Tests.Loans.Helpers;

namespace Tests.Loans.UnitTests;

public class TestLoanIntegration : IClassFixture<ApiWebApplicationFactory>
{

    private readonly HttpClient _client;

    public TestLoanIntegration(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllLoans_LoansFound_ReturnsOkStatusCode()
    {
        
        var request = "/api/v1/ControllerCustomer/CreateCustomer";
        var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
        var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
        
        var createLoanRequest = TestLoanFactory.CreateLoan(1);
         content = new StringContent(JsonConvert.SerializeObject(createLoanRequest), Encoding.UTF8, "application/json");
        await _client.GetAsync("/api/v1/ControllerLoan/all");

       response = await _client.GetAsync("/api/v1/ControllerLoan/all");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllLoans_LoansFound_ReturnsNotFoundStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/ControllerLoans/all");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetLoanById_LoanFound_ReturnsOkStatusCode()
    {
        var createLoanRequest = TestLoanFactory.CreateLoan(1);
        var content = new StringContent(JsonConvert.SerializeObject(createLoanRequest), Encoding.UTF8, "application/json");

        var createCustomer = new CreateCustomerRequest {Username = "test123",Name = "test",Email = "asda@gma.com",Password = "aASda123@",PhoneNumber = "0777777777"};
        var contentCustomer = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");
        var responseCustomer = await _client.PostAsync("/api/v1/ControllerCustomer/CreateCustomer",contentCustomer);
        var responseCustomerString = await responseCustomer.Content.ReadAsStringAsync();
        var resultCustomer = JsonConvert.DeserializeObject<CustomerResponse>(responseCustomerString);
        string token = resultCustomer.Token;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

        var createCategory = "test";
        var contentCategory = new StringContent(JsonConvert.SerializeObject(createCategory), Encoding.UTF8, "application/json");
        var responseCategory = await _client.PostAsync("/api/v1/ControllerCategory/CreateCategory",contentCategory);
        var responseCategoryString = await responseCategory.Content.ReadAsStringAsync();
        var resultCategory = JsonConvert.DeserializeObject<CategoryResponse>(responseCategoryString);
        
        var createBook = new CreateBookRequest { Title = "standard",Author = "asdasadom",NameCategory = "ASd",AvailableCopies = 100};
        var contentBook = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");
        var responseBook = await _client.PostAsync("/api/v1/ControllerBook/CreateBook",contentBook);
        var responseBookString = await responseBook.Content.ReadAsStringAsync();
        var resultBook = JsonConvert.DeserializeObject<BookResponse>(responseBookString);

        string name = resultCategory.Name;
        var contentAddCategory = new StringContent(JsonConvert.SerializeObject(name), Encoding.UTF8, "application/json");
        var responseAddCategory = await _client.PutAsync($"/api/v1/ControllerBook/AddCategory?id={resultBook.Id}",contentAddCategory);
        var respinseAddCategoryString = await responseAddCategory.Content.ReadAsStringAsync();
        var resultAddCategory = JsonConvert.DeserializeObject<BookResponse>(respinseAddCategoryString);

        var addloan = "test";
        var contentLoan = new StringContent(JsonConvert.SerializeObject(addloan), Encoding.UTF8, "application/json");
        var response=  await _client.PutAsync($"/api/v1/ControllerCustomer/AddLoan?id={resultCustomer.Id}",contentLoan);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CustomerResponse>(responseString);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }

    [Fact]
    public async Task GetLoanById_LoanNotFound_ReturnsNotFoundStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/ControllerLoan/delete/1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}