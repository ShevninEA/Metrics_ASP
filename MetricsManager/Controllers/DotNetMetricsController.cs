using MetricsManager.Models.Requests.Cpu;
using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Services.Client.Impl;

namespace MetricsManager.Controllers
{
    [Route("api/dotnet")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {
        #region Services

        private IMetricsAgentClient _metricsAgentClient;

        #endregion

        public DotnetMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }
        

        [HttpGet("get-all-by-id")]
        public ActionResult<DotnetMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok(_metricsAgentClient.GetDotnetMetrics(new DotnetMetricsRequest
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
