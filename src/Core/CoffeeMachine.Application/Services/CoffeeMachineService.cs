namespace CoffeeMachine.Application.Services
{
    public class CoffeeMachineService : ICoffeeMachineService
    {
        private readonly IWeatherService _weatherService;
        private readonly IConfiguration _configuration;

        public CoffeeMachineService(IWeatherService weatherService, IConfiguration configuration)
        {
            _weatherService = weatherService;
            _configuration = configuration;
        }

        public async Task<ResponseMessage> GetBrewCoffeeAsync()
        {
            string coffeeMachineMessage = "Your piping hot coffee is ready";
            var res = await _weatherService.GetRealTimeWeatherData();

            if (res != null && res.CurrentTemperatureCelcius > Convert.ToDouble(_configuration[ApplicationConstants.HotTemperatureThreshold]))
                coffeeMachineMessage = "Your refreshing iced coffee is ready";

            return await Task.FromResult(new ResponseMessage
            {
                message = coffeeMachineMessage,
                prepared = DateTime.Now
            });
        }
    }
}
