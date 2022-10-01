using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;

        public NetworkMetricsController(INetworkMetricsRepository networkMetricsRepository,
                    ILogger<NetworkMetricsController> logger)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation("Create network metric.");
            _networkMetricsRepository.Create(new Models.NetworkMetrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetrics>> GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get network metrics call.");
            return Ok(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }

        [HttpGet("getall")]
        public ActionResult<IList<NetworkMetrics>> GetAllNetworkMetrics()
        {
            _logger.LogInformation("Get all network metrics.");
            return Ok(_networkMetricsRepository.GetAll());
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<IList<NetworkMetrics>> DeleteNetworkMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete network metrics.");
            _networkMetricsRepository.Delete(id);
            return Ok();
        }

        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<NetworkMetrics>> GetByIdNetworkMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id network metrics.");
            return Ok(_networkMetricsRepository.GetById(id));
        }

        [HttpPut("update")]
        public ActionResult<IList<NetworkMetrics>> UpdateNetworkMetrics([FromBody] NetworkMetrics item)
        {
            _logger.LogInformation("Update network metrics.");
            _networkMetricsRepository.Update(item);
            return Ok();
        }
    }
}

