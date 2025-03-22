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

        public static IConfiguration CreateIConfigurationMock(int day, int month)
        {
            var inMemoryConfigSettings = new Dictionary<string, string> {
                {"MonthOfServiceUnavailability:Day", Convert.ToString(day)},
                {"MonthOfServiceUnavailability:Month", Convert.ToString(month)}
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
    }
}
