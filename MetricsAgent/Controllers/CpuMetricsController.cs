using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository cpuMetricsRepository,
                    ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
            _mapper = mapper; 
        }

        ////Более не нужен
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetCpuMetricsResponse> GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");

            return Ok(new GetCpuMetricsResponse
            {
                Metrics = _mapper.Map<List<CpuMetricDto>>(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime))
            });
        }

        [HttpGet("getall")]
        public ActionResult<IList<CpuMetricDto>> GetAllCpuMetrics()
        {
            _logger.LogInformation("Get all cpu metrics.");
            return Ok(_mapper.Map<List<CpuMetricDto>>(_cpuMetricsRepository.GetAll()));
        }

        ////Более не нужен
        //[HttpDelete("delete/{id}")]
        //public ActionResult<IList<CpuMetricDto>> DeleteCpuMetrics([FromRoute] int id)
        //{
        //    _logger.LogInformation("Delete cpu metrics.");
        //    _cpuMetricsRepository.Delete(id);
        //    return Ok();
        //}

        ////Более не нужен
        //[HttpGet("getbyid/{id}")]
        //public ActionResult<IList<CpuMetricDto>> GetByIdCpuMetrics([FromRoute] int id)
        //{
        //    _logger.LogInformation("Get by id cpu metrics.");
        //    return Ok(_cpuMetricsRepository.GetById(id));
        //}

        ////Более не нужен
        //[HttpPut("update")]
        //public ActionResult<IList<CpuMetricDto>> UpdateCpuMetrics([FromBody] CpuMetric item)
        //{
        //    _logger.LogInformation("Update cpu metrics.");
        //    _cpuMetricsRepository.Update(item);
        //    return Ok();
        //}
    }
}
