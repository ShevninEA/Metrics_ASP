using MetricsManager.Models;
using MetricsManager.Services;
using MetricsManager.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private AgentPool _agentPool;
        private readonly IAgentRepository _agentRepository;

        public AgentsController(AgentPool agentPool, IAgentRepository agentRepository)
        {
            _agentPool = agentPool;
            _agentRepository = agentRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            if (agentInfo != null)
            {
                _agentRepository.Create(agentInfo);
            }
            return Ok();
        }

        [HttpGet("getById")]
        public ActionResult GetAgentById(int id)
        {
            return Ok(_agentRepository.GetById(id));
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Values.ContainsKey(agentId))
                _agentPool.Values[agentId].Enable = true;
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Values.ContainsKey(agentId))
                _agentPool.Values[agentId].Enable = false;
            return Ok();
        }

        [HttpDelete("delete/{agentId}")]
        public ActionResult Delete([FromRoute] int agentId)
        {
            _agentRepository.Delete(agentId);
            return Ok();
        }

        [HttpGet("getAll")]
        public ActionResult GetallAgents()
        {
            return Ok(_agentRepository.GetAll());
        }
    }
}
