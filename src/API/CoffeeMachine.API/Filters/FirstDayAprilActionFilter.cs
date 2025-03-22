namespace CoffeeMachine.API.Filters
{
    public class FirstDayAprilActionFilter : ActionFilterAttribute//Attribute, IAsyncActionFilter
    {
        private IConfiguration? _configuration;

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if (DateTime.Now.Day == _configuration?.GetValue<int>(ApplicationConstants.DayOfServiceUnavailability) 
                && DateTime.Now.Month == _configuration.GetValue<int>(ApplicationConstants.MonthOfServiceUnavailability))
            {
                throw new FirstDayOfAprilException(nameof(StatusCodes.Status418ImATeapot));
            }

            await next();
        }
    }
}
