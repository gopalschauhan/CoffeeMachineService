namespace CoffeeMachine.Test.Tests.Core
{
    public class CoffeeMachineServiceTest
    {
        [Fact]
        public async void GetBrewCoffeeAsyncTest()
        {
            var service = new CoffeeMachineService();

            var response = await service.GetBrewCoffeeAsync();

            Assert.NotNull(response);
            Assert.True(response.message == "Your piping hot coffee is ready");
        }
    }
}
