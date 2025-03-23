using CoffeeMachine.API.IntegrationTest;
using CoffeeMachine.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace CoffeeMachine.Test.Integration
{
    public class CoffeeMachineAPITests : IClassFixture<WebApplicationFactory<IApiHostMaker>>
    {
        private readonly WebApplicationFactory<IApiHostMaker> _webApplicationFactory;

        public CoffeeMachineAPITests(WebApplicationFactory<IApiHostMaker> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async void GetBrewCoffeeAsyncEndpointTest()
        {
            var httpClient = _webApplicationFactory.CreateClient();

            var apiCalls = new[] {
                httpClient.GetAsync(httpClient.BaseAddress + "brew-coffee"),
                httpClient.GetAsync(httpClient.BaseAddress + "brew-coffee"),
                httpClient.GetAsync(httpClient.BaseAddress + "brew-coffee"),
                httpClient.GetAsync(httpClient.BaseAddress + "brew-coffee"),
                httpClient.GetAsync(httpClient.BaseAddress + "brew-coffee")
            };

            var response = await Task.WhenAll(apiCalls);

            var successResult = response.Where(r => r.StatusCode == HttpStatusCode.OK);
            var errorResult = response.Where(r => r.StatusCode == HttpStatusCode.ServiceUnavailable);
            var contents = await successResult.FirstOrDefault()!.Content.ReadFromJsonAsync<ResponseMessage>();

            Assert.NotNull(successResult);
            Assert.True(successResult.Count() == 4);
            Assert.NotNull(contents);
            Assert.True(successResult.FirstOrDefault()!.IsSuccessStatusCode);
            Assert.True(contents.message == "Your piping hot coffee is ready" || contents.message == "Your refreshing iced coffee is ready") ;

            Assert.NotNull(errorResult);
            Assert.True(!errorResult.FirstOrDefault()?.IsSuccessStatusCode);
            Assert.True(errorResult.FirstOrDefault()?.StatusCode == HttpStatusCode.ServiceUnavailable);
        }
    }
}
