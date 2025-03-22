namespace CoffeeMachine.Application.Services
{
    public class CoffeeMachineService : ICoffeeMachineService
    {
        public async Task<ResponseMessage> GetBrewCoffeeAsync()
        {
            return await Task.FromResult(new ResponseMessage
            {
                message = "Your piping hot coffee is ready",
                prepared = DateTime.Now
            });
        }
    }
}
