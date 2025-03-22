namespace CoffeeMachine.Application.Models
{
    public class ResponseMessage
    {
        public string message { get; set; } = string.Empty;
        public DateTime prepared { get; set; }
    }
}
