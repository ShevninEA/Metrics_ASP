using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route(" api/metrics/ram/available")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;

        public RamMetricsController(IRamMetricsRepository ramMetricsRepository,
                    ILogger<RamMetricsController> logger)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Create ram metric.");
            _ramMetricsRepository.Create(new Models.RamMetrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetrics>> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics call.");
            return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }

        [HttpGet("getall")]
        public ActionResult<IList<RamMetrics>> GetAllRamMetrics()
        {
            _logger.LogInformation("Get all ram metrics.");
            return Ok(_ramMetricsRepository.GetAll());
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<IList<RamMetrics>> DeleteRamMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete ram metrics.");
            _ramMetricsRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<RamMetrics>> GetByIdRamMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id ram metrics.");
            return Ok(_ramMetricsRepository.GetById(id));
        }

        /// <summary>
        /// Также почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("update/{item}")]
        public ActionResult<IList<RamMetrics>> UpdateRamMetrics([FromRoute] RamMetrics item)
        {
            _logger.LogInformation("Update ram metrics.");
            _ramMetricsRepository.Update(item);
            return Ok();
        }
    }
}
