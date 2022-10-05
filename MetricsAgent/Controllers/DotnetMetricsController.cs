using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Services.Impl;
using AutoMapper;
using MetricsAgent.Models.Dto;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors - count")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotnetMetricsRepository _dotnetMetricsRepository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(IDotnetMetricsRepository dotnetMetricsRepository,
                    ILogger<DotNetMetricsController> logger, IMapper mapper)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            _logger.LogInformation("Create dotnet metric.");
            _dotnetMetricsRepository.Create(_mapper.Map<DotnetMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<DotnetMetricDto>> GetDotnetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get dotnet metrics call.");
            return Ok(_mapper.Map<List<DotnetMetricDto>>(_dotnetMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("getall")]
        public ActionResult<IList<DotnetMetricDto>> GetAllDotnetMetrics()
        {
            _logger.LogInformation("Get all dotnet metrics.");
            return Ok(_mapper.Map<List<DotnetMetricDto>>(_dotnetMetricsRepository.GetAll()));
        }

        //Более не нужен
        [HttpDelete("delete/{id}")]
        public ActionResult<IList<DotnetMetricDto>> DeleteDotnetMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete dotnet metrics.");
            _dotnetMetricsRepository.Delete(id);
            return Ok();
        }

        //Более не нужен
        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<DotnetMetricDto>> GetByIdDotnetMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id dotnet metrics.");
            return Ok(_dotnetMetricsRepository.GetById(id));
        }

        //Более не нужен
        [HttpPut("update")]
        public ActionResult<IList<DotnetMetricDto>> UpdateDotnetMetrics([FromBody] DotnetMetric item)
        {
            _logger.LogInformation("Update dotnet metrics.");
            _dotnetMetricsRepository.Update(item);
            return Ok();
        }
    }
}
