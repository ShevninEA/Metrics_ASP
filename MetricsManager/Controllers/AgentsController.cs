using MetricsManager.Models;
using MetricsManager.Services;
using MetricsManager.Services.Impl;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        [HttpPost("register")]
        public ActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
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
        public ActionResult EnableAgentById([FromRoute] int agentId)
        {
            if (_agentRepository != null)
                _agentRepository.Enable(agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public ActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_agentRepository != null)
                _agentRepository.Disable(agentId);
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
