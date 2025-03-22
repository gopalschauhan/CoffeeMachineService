using CoffeeMachine.Application.Models;

namespace CoffeeMachine.Application.Contracts
{
    public interface ICoffeeMachineService
    {
        Task<ResponseMessage> GetBrewCoffeeAsync();
    }
}
