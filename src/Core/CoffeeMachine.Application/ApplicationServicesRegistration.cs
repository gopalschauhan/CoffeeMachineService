namespace CoffeeMachine.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection RegisterApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ICoffeeMachineService, CoffeeMachineService>();
            return services;
        }
    }
}
