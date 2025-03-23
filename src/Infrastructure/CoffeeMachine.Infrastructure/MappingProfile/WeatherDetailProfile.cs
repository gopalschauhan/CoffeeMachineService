namespace CoffeeMachine.Infrastructure.MappingProfile
{
    public class WeatherDetailProfile : Profile
    {
        public WeatherDetailProfile()
        {
            CreateMap<RealTimeWeatherDetails, WeatherDetails>()
                .ForMember(wd => wd.City, opt => opt.MapFrom(src => src.location.name))
                .ForMember(wd => wd.Country, opt => opt.MapFrom(src => src.location.country))
                .ForMember(wd => wd.Latitude, opt => opt.MapFrom(src => src.location.lat))
                .ForMember(wd => wd.Longitude, opt => opt.MapFrom(src => src.location.lon))
                .ForMember(wd => wd.CurrentTemperatureCelcius, opt => opt.MapFrom(src => src.current.temp_c))
                .ForMember(wd => wd.CurrentTemperatureFahrenheit, opt => opt.MapFrom(src => src.current.temp_f));
        }
    }
}
