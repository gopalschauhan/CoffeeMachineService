namespace CoffeeMachine.Test.Tests.API
{
    public class CoffeeMachineTest
    {
        [Fact]
        public async void CoffeeControllerGetTest() 
        {
            var controller = new CoffeeMachineController(MockObjectFactory.CreateCoffeeMachineMock().Object);
            var result = await controller.Get();

            Assert.NotNull(result);
            Assert.True(((ObjectResult?)result.Result)?.StatusCode == 200);
            Assert.True(((ResponseMessage)((ObjectResult)result.Result).Value).message == "coffee is ready");
        }
    }
}
