using MetricsManager.Models.Requests.Cpu;
using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Services.Client.Impl;

namespace MetricsManager.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        #region Services

        private IMetricsAgentClient _metricsAgentClient;

        #endregion

        public NetworkMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        [HttpGet("get-all-by-id")]
        public ActionResult<NetworkMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok(_metricsAgentClient.GetNetworkMetrics(new NetworkMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }

        #region Old-cotrollers

        //[HttpGet("all/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAll(
        //    [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    return Ok();
        //}

        #endregion
    }
}
