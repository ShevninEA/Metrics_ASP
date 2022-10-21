using MetricsManager.Models;
using MetricsManager.Models.Requests.Cpu;
using MetricsManager.Services.Client.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        #region Services

        private IMetricsAgentClient _metricsAgentClient;

        #endregion

        public CpuMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        [HttpGet("get-all-by-id")]
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok(_metricsAgentClient.GetCpuMetrics(new CpuMetricsRequest
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
