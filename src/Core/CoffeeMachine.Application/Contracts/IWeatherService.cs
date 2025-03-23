namespace CoffeeMachine.Application.Contracts
{
    public interface IWeatherService
    {
        Task<WeatherDetails> GetRealTimeWeatherData();
    }
}
