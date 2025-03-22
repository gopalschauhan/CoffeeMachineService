namespace CoffeeMachine.API.Controllers
{
    [Route("/[Action]")]
    [ApiController]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineService _coffeeMachineService;

        public CoffeeMachineController(ICoffeeMachineService coffeeMachineService)
        {
            _coffeeMachineService = coffeeMachineService;
        }

        [FirstDayAprilActionFilter]
        [EnableRateLimiting("fixed")]
        [HttpGet(Name = "brew-coffee")]
        [ActionName("brew-coffee")]
        [ProducesResponseType(typeof(ResponseMessage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status418ImATeapot)]
        public async Task<ActionResult<ResponseMessage>> Get()
        {
            return Ok(await _coffeeMachineService.GetBrewCoffeeAsync());
        }
    }
}
