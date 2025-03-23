namespace CoffeeMachine.Test.Tests.Core
{
    public class CoffeeMachineServiceTest
    {
        [Fact]
        public async void GetBrewCoffeeAsyncHotWeatherTest()
        {
            var service = new CoffeeMachineService(MockObjectFactory.CreateWeatherServiceMock(32).Object,
                MockObjectFactory.CreateIConfigurationMock(1, 4));

            var response = await service.GetBrewCoffeeAsync();

            Assert.NotNull(response);
            Assert.True(response.message == "Your refreshing iced coffee is ready");
        }

        [Fact]
        public async void GetBrewCoffeeAsyncColdWeatherTest()
        {
            var service = new CoffeeMachineService(MockObjectFactory.CreateWeatherServiceMock(20).Object,
                MockObjectFactory.CreateIConfigurationMock(1, 4));

            var response = await service.GetBrewCoffeeAsync();

            Assert.NotNull(response);
            Assert.True(response.message == "Your piping hot coffee is ready");
        }
    }
}
