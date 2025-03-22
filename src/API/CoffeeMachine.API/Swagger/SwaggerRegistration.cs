namespace CoffeeMachine.API.Swagger
{
    public static class SwaggerRegistration
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Coffee Machine Service",
                    Version = "v1"
                });
            });
        }
    }
}
