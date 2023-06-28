using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
        public class WeatherData
        {
            public int Id { get; set; }
            public string Date { get; set; }
            public int Degree { get; set; }
            public string Location { get; set; }
        }

        [ApiController]
        [Route("[controller]")]
        public class WeatherForecastController : ControllerBase
        {
            private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

            public static List<WeatherData> weatherDatas = new()
        {
            new WeatherData() { Id = 1, Date = "05.10.2002", Degree = 12, Location="CA" },
            new WeatherData() { Id = 2, Date = "05.05.2002", Degree = -2, Location = "LA" },
            new WeatherData() { Id = 3, Date = "31.10.2022", Degree = -10, Location = "NY" }
        };


            private readonly ILogger<WeatherForecastController> _logger;

            public WeatherForecastController(ILogger<WeatherForecastController> logger)
            {
                _logger = logger;
            }

            [HttpGet]
            public List<WeatherData> Get()
            {
                return weatherDatas;
            }

            [HttpPost]
            public IActionResult Add(WeatherData data)
            {
                if (data == null)
                {
                    return BadRequest("NULL");
                }

                for (int i = 0; i < weatherDatas.Count; i++)
                {
                    if (weatherDatas[i].Id == data.Id)
                    {
                        return BadRequest("ID ALREADY EXISTS");
                    }
                }
                weatherDatas.Add(data);
                return Ok();
            }

            [HttpPut]
            public IActionResult Update(WeatherData data)
            {
                for (int i = 0; i < weatherDatas.Count; i++)
                {
                    if (weatherDatas[i].Id == data.Id)
                    {
                        weatherDatas[i] = data;
                        return Ok();
                    }
                }

                return BadRequest("ID NOT FOUND");
            }

            [HttpDelete]
            public IActionResult Delete(int id)
            {
                for (int i = 0; i < weatherDatas.Count; i++)
                {
                    if (weatherDatas[i].Id == id)
                    {
                        weatherDatas.RemoveAt(i);
                        return Ok();
                    }
                }

                return BadRequest("ID NOT FOUND");
            }

            [HttpGet("{id}")]
            public IActionResult GetName(int id)
            {
                if (id < 0 || id >= Summaries.Count)
                {
                    return BadRequest("BAD INDEX");
                }

                for (int i = 0; i < weatherDatas.Count; i++)
                {
                    if (weatherDatas[i].Id == id)
                    {
                        return Ok(weatherDatas[i]);
                    }
                }

                return BadRequest("Item not found!");
            }

            [HttpGet("find-by-city")]
            public IActionResult GetByCity(string location)
            {
                for (int i = 0; i < weatherDatas.Count; i++)
                {
                    if (weatherDatas[i].Location == location)
                    {
                        return Ok("Item found!");
                    }
                }

                return BadRequest("Item not found!");
            }
        }
    }
