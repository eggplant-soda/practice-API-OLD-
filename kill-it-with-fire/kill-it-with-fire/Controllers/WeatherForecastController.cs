using Microsoft.AspNetCore.Mvc;

namespace kill_it_with_fire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            if (name == null)
            {
                return BadRequest("somethin' needs to be typed in 'ere");
            }
            Summaries.Add(name);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("jesse wheres the right index jesse");
            }
            if (name == null)
            {
                return BadRequest("somethin' needs to be typed in 'ere");
            }
            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("jesse stop headbanging your keyboard and type an existing index");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
        // 2
        [HttpGet("{index}")]
        public string GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return "jesse wheres the right index jesse";
            }
            return Summaries[index];
        }
        //3
        [HttpGet("find-by-name")]
        public int GetByName(string name)
        {
            return Summaries.Count(x => x == name);
        }
        //4
        [HttpGet("getall")]
        public IActionResult GetAll(int? sortStrategy)
        {
            try
            {
                if (sortStrategy == 1)
                {
                    Summaries.Sort();
                }
                if (sortStrategy == -1)
                {
                    Summaries.Sort();
                    Summaries.Reverse();
                }
            }
            catch
            {
                if (sortStrategy == 0 || sortStrategy > 1 || sortStrategy < -1)
                {
                    return BadRequest("oi mate stop doing cringey stuff");
                }
            }
            return Ok(Get());

        }
    }
}