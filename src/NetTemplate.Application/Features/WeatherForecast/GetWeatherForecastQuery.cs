using MediatR;
using NetTemplate.Application.DTOs;

namespace NetTemplate.Application.Features.WeatherForecast
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecastDto>>
    {
    }
}
