namespace CoffeeMachine.API.APIPolicies
{
    public static class APIRateLimitPolicy
    {
        public static IServiceCollection RegisterRateLimitPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status503ServiceUnavailable;
                options.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = configuration.GetValue<int>(ApplicationConstants.APIPermitRateLimit); //allowing 4 calls
                    options.QueueLimit = configuration.GetValue<int>(ApplicationConstants.APIRateLimitQueue); // No queueing
                    options.Window = TimeSpan.FromSeconds(configuration.GetValue<int>(ApplicationConstants.APIRateLimitWindow)); // Within 60 seconds
                    options.AutoReplenishment = true;
                });
            });

            return services;
        }
    }
}
