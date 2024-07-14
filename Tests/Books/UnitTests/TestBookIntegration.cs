using Newtonsoft.Json;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Models;
using OnlineBookShop.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Tests.Books.Helpers;
using Tests.Infrastucture;

namespace Tests.Books.UnitTests
{
    public class TestBookIntegration : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TestBookIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllBooks_BooksFound_ReturnsOkStatusCode_ValidResponse()
        {
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            var createBookRequest = TestBookFactory.CreateBook(1);
            content = new StringContent(JsonConvert.SerializeObject(createBookRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerBook/createBook", content);

             response = await _client.GetAsync("/api/v1/ControllerBook/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBookById_BookFound_ReturnsOkStatusCode_ValidResponse()
        {
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            var createBook = new CreateBookRequest { Title = "New Book 1", Author = "asdsdf", AvailableCopies = 100, NameCategory = "sad", PublicationYear = 2000 };
            content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");
            var res =  await _client.PostAsync("/api/v1/ControllerBook/CreateBook", content);
            var resString = await res.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookResponse>(resString);
            
            response = await _client.GetAsync($"/api/v1/ControllerBook/FindById?id={result.Id}");
            resString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<BookResponse>(resString);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Title,createBook.Title);
        }

        [Fact]
        public async Task GetBookById_BookNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerBook/findById/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            request = "/api/v1/ControllerBook/createBook";
            var ControllerBook = new CreateBookRequest { Title = "New Book 1", Author = "asdsdf", AvailableCopies = 100, NameCategory = "sad", PublicationYear = 2000 };
            content = new StringContent(JsonConvert.SerializeObject(ControllerBook), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Book>(responseString);

            Assert.NotNull(result);
            Assert.Equal(ControllerBook.Title, result.Title);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            request = "/api/v1/ControllerBook/CreateBook";
            var createBook = new CreateBookRequest { Title = "New Book 1", Author = "asdsdf", AvailableCopies = 100, NameCategory = "sad", PublicationYear = 2000 };
            content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

           response = await _client.PostAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookResponse>(responseString)!;

            request = $"/api/v1/ControllerBook/UpdateBook?id={result.Id}";
            var updateBook = new UpdateBookRequest { AvailableCopies = 100 };
            content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<BookResponse>(responseString);
            Assert.Equal(response.StatusCode,HttpStatusCode.OK);
            Assert.Equal(updateBook.Title, result.Title);
        }

        [Fact]
        public async Task Put_Update_InvalidBookTitle_ReturnsBadRequestStatusCode()
        {
            
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
             request = "/api/v1/ControllerBook/CreateBook";
            var createBook = new CreateBookRequest { Title = "New Book 1", Author = "asdsdf", AvailableCopies = 100, NameCategory = "sad", PublicationYear = 2000 };
            content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

           response = await _client.PostAsync(request, content);
             responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookResponse>(responseString)!;

            request = $"/api/v1/ControllerBook/UpdateBook?id={result.Id}";
            var updateBook = new UpdateBookRequest { Title = "" };
            content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result.Title,updateBook.Title);
        }

        [Fact]
        public async Task Put_Update_BookDoesNotExist_ReturnsNotFoundStatusCode()
        {
            
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
             request = "/api/v1/ControllerBook/updateBook";
            var updateBook = new UpdateBookRequest { Title = "New Book 1", Author = "asdsdf", AvailableCopies = 100, NameCategory = "sad", PublicationYear = 2000 };
             content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

             response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_BookExists_ReturnsDeletedBook()
        {
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            request = "/api/v1/ControllerBook/CreateBook";
            var createBook = new CreateBookRequest { Title = "New Book 1", Author = "asdsdf", AvailableCopies = 100, NameCategory = "sad", PublicationYear = 2000 };
             content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookResponse>(responseString)!;

            request = $"/api/v1/ControllerBook/DeleteBook?id={result.Id}";

            response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }

        [Fact]
        public async Task Delete_Delete_BookDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerBook/DeleteBook/77777";

            var response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
