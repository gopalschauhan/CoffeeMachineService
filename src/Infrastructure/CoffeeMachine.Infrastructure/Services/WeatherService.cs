namespace CoffeeMachine.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public WeatherService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<WeatherDetails> GetRealTimeWeatherData()
        {
            WeatherDetails weather = new WeatherDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Convert.ToString(_configuration[ApplicationConstants.WeatherAPIUrl]));

                var weatherDetails = await httpClient.GetFromJsonAsync<RealTimeWeatherDetails>("");

                if (weatherDetails != null)
                {
                    weather = _mapper.Map<RealTimeWeatherDetails, WeatherDetails>(weatherDetails);
                }
            }
            return weather;
        }
    }
}
