namespace CoffeeMachine.Test.Mock
{
    public class MockObjectFactory
    {
        public static Mock<ICoffeeMachineService> CreateCoffeeMachineMock()
        {
            var response = new ResponseMessage
            {
                message = "coffee is ready",
                prepared = DateTime.Now
            };

            var mockCoffeeMachineService = new Mock<ICoffeeMachineService>();
            mockCoffeeMachineService.Setup(cfe => cfe.GetBrewCoffeeAsync()).Returns(Task.FromResult(response));

            return mockCoffeeMachineService;
        }

        public static Mock<IWeatherService> CreateWeatherServiceMock(double currentTemperature)
        {
            var response = new WeatherDetails
            {
                City = "Melbourne",
                Country = "Australia",
                Longitude = 43423.334,
                Latitude = 343.545,
                CurrentTemperatureCelcius = currentTemperature,
                CurrentTemperatureFahrenheit = 60
            };

            var mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService.Setup(ws => ws.GetRealTimeWeatherData()).Returns(Task.FromResult(response));

            return mockWeatherService;
        }

        public static IConfiguration CreateIConfigurationMock(int day, int month, int temperatureThreshold = 30)
        {
            var inMemoryConfigSettings = new Dictionary<string, string> {
                {"MonthOfServiceUnavailability:Day", Convert.ToString(day)},
                {"MonthOfServiceUnavailability:Month", Convert.ToString(month)},
                {"WeatherAPIUrl", "https://api.weatherapi.com/v1/current.json?q=melbourne&key=17d2a82abb2c4d4aae4151745252203"},
                {"HotTemperatureThreshold", Convert.ToString(temperatureThreshold)}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemoryConfigSettings!).Build();

            return configuration;
        }

        public static DefaultHttpContext CreateDefaultHttpContextMock(int day, int month)
        {
            var services = new ServiceCollection();
            services.AddSingleton(CreateIConfigurationMock(day, month));
            var serviceProvider = services.BuildServiceProvider();

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };

            return httpContext;
        }

        public static ActionExecutingContext CreateActionExecutingContextMock(int day, int month)
        {
            var context = new ActionExecutingContext(
                new ActionContext(CreateDefaultHttpContextMock(day, month), new RouteData(), new ActionDescriptor()),
                new List<IFilterMetadata>(),
                new Dictionary<string, object>()!,
                controller: null!);

            return context;
        }

        public static ActionExecutionDelegate CreateActionExecutionDelegateMock(ActionExecutingContext context)
        {
            var next = new ActionExecutionDelegate(() =>
            {
                Task.Delay(100).Wait();
                return Task.FromResult(new ActionExecutedContext(context, new List<IFilterMetadata>(), null));
            });
            return next;
        }

        public static RequestDelegate CreateRequestDelegateMock(int statusCode)
        {
            var requestDelegate = new RequestDelegate((context) =>
            {
                switch (statusCode) 
                {
                    case 418:
                        context.Response.StatusCode = StatusCodes.Status418ImATeapot;
                        return Task.FromException(new FirstDayOfAprilException("Status418ImATeapot"));
                    case 500:
                        context.Response.StatusCode = StatusCodes.Status418ImATeapot;
                        return Task.FromException(new Exception("500InternalServerError"));
                    default:
                        return Task.CompletedTask;
                }
            });

            return requestDelegate;
        }

        public static Mock<IMapper> CreateIMapperMock() 
        {
            var automapper = new Mock<IMapper>();

            automapper.Setup(am => am.Map<object, object>(It.IsAny<object>, It.IsAny<object>)).Returns(new WeatherDetails
            {
                City = "Melbourne",
                Country = "Australia",
                Longitude = 43423.334,
                Latitude = 343.545,
                CurrentTemperatureCelcius = 30,
                CurrentTemperatureFahrenheit = 60
            });
            return automapper;
        }
    }
}
