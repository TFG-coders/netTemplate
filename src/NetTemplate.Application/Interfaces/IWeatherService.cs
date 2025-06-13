using NetTemplate.Application.DTOs;

namespace NetTemplate.Application.Interfaces
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecastDto> GetForecast();
    }
}
