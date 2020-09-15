using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Challenge.Services.Payment.Model.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Challenge.Services.Payment.Test.Helper
{
    public class IntegrationTestBase
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTestBase()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {   
                    builder.ConfigureServices(services =>
                    {
                        
                    });
                });

            TestClient = appFactory.CreateClient();
        }


        protected async Task<CreatePaymentResponseModel> PostAsync<TRequest>(TRequest request,string url)
        {
            var response = await TestClient.PostAsJsonAsync(url, request);
            var result =  await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreatePaymentResponseModel>(result);
        }
        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await TestClient.GetAsync(url);
            return await response.Content.ReadAsAsync<TResponse>();
        }
        protected async Task<HttpStatusCode> UpdateAsync<TRequest>(TRequest request, string url)
        {
            var response = await TestClient.PutAsJsonAsync(url,request);
                return response.StatusCode;
        }
        protected async Task<HttpStatusCode> DeleteAsync( string url)
        {
            var response = await TestClient.DeleteAsync(url);
            return response.StatusCode;
        }
    }
}
