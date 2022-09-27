using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd/left")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricsRepository;

        public HddMetricsController(IHddMetricsRepository hddMetricsRepository,
                    ILogger<HddMetricsController> logger)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation("Create hdd metric.");
            _hddMetricsRepository.Create(new Models.HddMetrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<HddMetrics>> GetHddMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get hdd metrics call.");
            return Ok(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }

        [HttpGet("getall")]
        public ActionResult<IList<HddMetrics>> GetAllHddMetrics()
        {
            _logger.LogInformation("Get all hdd metrics.");
            return Ok(_hddMetricsRepository.GetAll());
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<IList<HddMetrics>> DeleteHddMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete hdd metrics.");
            _hddMetricsRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<HddMetrics>> GetByIdHddMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id hdd metrics.");
            return Ok(_hddMetricsRepository.GetById(id));
        }

        /// <summary>
        /// Также почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("update/{item}")]
        public ActionResult<IList<HddMetrics>> UpdateHddMetrics([FromRoute] HddMetrics item)
        {
            _logger.LogInformation("Update hdd metrics.");
            _hddMetricsRepository.Update(item);
            return Ok();
        }
    }
}
