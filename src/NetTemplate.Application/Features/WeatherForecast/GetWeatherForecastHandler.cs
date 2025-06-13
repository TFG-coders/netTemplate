using MediatR;
using NetTemplate.Application.DTOs;
using NetTemplate.Application.Interfaces;

namespace NetTemplate.Application.Features.WeatherForecast
{
    public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<WeatherForecastDto>>
    {
        private readonly IWeatherService _weatherService;

        public GetWeatherForecastHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public Task<IEnumerable<WeatherForecastDto>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var forecast = _weatherService.GetForecast();
            return Task.FromResult(forecast);
        }
    }
}
