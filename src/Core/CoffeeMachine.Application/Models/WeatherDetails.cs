namespace CoffeeMachine.Application.Models
{
    public class WeatherDetails
    {
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double CurrentTemperatureCelcius { get; set; }
        public double CurrentTemperatureFahrenheit { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
