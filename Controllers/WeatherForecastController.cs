using Microsoft.AspNetCore.Mvc;

namespace HanderBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHandlerService _handlerService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHandlerService handlerService)
        {
            _logger = logger;
            _handlerService = handlerService;
        }

        [HttpGet("/WeatherForecast/GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("/WeatherForecast/GetSummarizeBook")]
        public async Task<IActionResult> SummarizeBook(IFormFile file)
        {
            var extractedTextSummarize = await _handlerService.SummarizeBook(file);
            return Ok(extractedTextSummarize);
        }


        [HttpPost("/WeatherForecast/GetShortStory")]
        public async Task<IActionResult> TransferSummarizeContentToShortStory(IFormFile file)
        {
            var extractedTextSummarize = await _handlerService.TransferSummarizeContentToShortStory(file);
            return Ok(extractedTextSummarize);
        }
    }
}
