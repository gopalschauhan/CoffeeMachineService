using CoffeeMachine.API.IntegrationTest;
using CoffeeMachine.Application.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

namespace CoffeeMachine.Test.Integration.Host
{
    public class CoffeeMachineAPITestHost : IClassFixture<WebApplicationFactory<IApiHostMaker>>
    {
        private readonly WebApplicationFactory<IApiHostMaker> _webApplicationFactory;

        public CoffeeMachineAPITestHost(WebApplicationFactory<IApiHostMaker> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        private HttpClient APIClient 
        {
            get { return _webApplicationFactory.CreateClient(); }
        }

        //[Fact]
        //public async void 

        ////[Fact]
        ////public async Task TestFirstmethos() 
        ////{
        ////    var httpClient = _webApplicationFactory.CreateClient();
        ////    HttpResponseMessage response = null;

        ////    for (int i = 0; i < 5; i++)
        ////    {
        ////        response = await httpClient.GetAsync(httpClient.BaseAddress + "brew-coffee");
        ////    }

        ////    //var response = await httpClient.GetAsync(httpClient.BaseAddress + "brew-coffee");
        ////    //var contents = await response?.Content.ReadFromJsonAsync<ResponseMessage>();

        ////    var contents = await response?.Content.ReadAsStringAsync();

        ////    //var response = await httpClient.GetFromJsonAsync(httpClient.BaseAddress + "brew-coffee", typeof(ResponseMessage), null, CancellationToken.None);
        ////}
    }
}
