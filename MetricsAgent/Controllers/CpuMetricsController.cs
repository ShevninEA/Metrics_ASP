using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        
        public CpuMetricsController(ICpuMetricsRepository cpuMetricsRepository,
                    ILogger<CpuMetricsController> logger)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _cpuMetricsRepository.Create(new Models.CpuMetrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetrics>> GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }

        [HttpGet("getall")]
        public ActionResult<IList<CpuMetrics>> GetAllCpuMetrics()
        {
            _logger.LogInformation("Get all cpu metrics.");
            return Ok(_cpuMetricsRepository.GetAll());
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<IList<CpuMetrics>> DeleteCpuMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete cpu metrics.");
            _cpuMetricsRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<CpuMetrics>> GetByIdCpuMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id cpu metrics.");
            return Ok(_cpuMetricsRepository.GetById(id));
        }

        /// <summary>
        /// Также почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("update/{item}")]
        public ActionResult<IList<CpuMetrics>> UpdateCpuMetrics([FromRoute] CpuMetrics item)
        {
            _logger.LogInformation("Update cpu metrics.");
            _cpuMetricsRepository.Update(item);
            return Ok();
        }
    }
}
