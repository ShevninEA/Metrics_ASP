using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Services.Impl;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors - count")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotnetMetricsRepository _dotnetMetricsRepository;

        public DotNetMetricsController(IDotnetMetricsRepository dotnetMetricsRepository,
                    ILogger<DotNetMetricsController> logger)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            _logger.LogInformation("Create dotnet metric.");
            _dotnetMetricsRepository.Create(new Models.DotnetMetrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<DotnetMetrics>> GetdDotnetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get dotnet metrics call.");
            return Ok(_dotnetMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }

        [HttpGet("getall")]
        public ActionResult<IList<DotnetMetrics>> GetAllDotnetMetrics()
        {
            _logger.LogInformation("Get all dotnet metrics.");
            return Ok(_dotnetMetricsRepository.GetAll());
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<IList<DotnetMetrics>> DeleteDotnetMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete dotnet metrics.");
            _dotnetMetricsRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<DotnetMetrics>> GetByIdDotnetMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id dotnet metrics.");
            return Ok(_dotnetMetricsRepository.GetById(id));
        }

        /// <summary>
        /// Также почему-то не работает, не смог разобраться
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("update/{item}")]
        public ActionResult<IList<DotnetMetrics>> UpdateDotnetMetrics([FromRoute] DotnetMetrics item)
        {
            _logger.LogInformation("Update dotnet metrics.");
            _dotnetMetricsRepository.Update(item);
            return Ok();
        }
    }
}
