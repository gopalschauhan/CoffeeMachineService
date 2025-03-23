namespace CoffeeMachine.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
