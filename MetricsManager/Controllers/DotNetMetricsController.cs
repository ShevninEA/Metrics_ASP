using MetricsManager.Models.Requests.Cpu;
using MetricsManager.Models;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/dotnet")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {
        #region Services

        private IHttpClientFactory _httpClientFactory;
        private AgentPool _agentPool;
        private IMetricsAgentClient _metricsAgentClient;

        #endregion

        public DotnetMetricsController(
            IMetricsAgentClient metricsAgentClient,
            IHttpClientFactory httpClientFactory,
            AgentPool agentPool)
        {
            _httpClientFactory = httpClientFactory;
            _metricsAgentClient = metricsAgentClient;
            _agentPool = agentPool;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<DotnetMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(_metricsAgentClient.GetDotnetMetrics(new DotnetMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }

        [HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAll(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
