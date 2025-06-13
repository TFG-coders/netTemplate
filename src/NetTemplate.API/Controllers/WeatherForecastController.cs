using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetTemplate.Application.DTOs;
using NetTemplate.Application.Features.WeatherForecast;

namespace NetTemplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecastDto>> Get()
        {
            return await _mediator.Send(new GetWeatherForecastQuery());
        }
    }
}
