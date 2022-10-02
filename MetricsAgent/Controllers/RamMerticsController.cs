﻿using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Services.Impl;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route(" api/metrics/ram/available")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository ramMetricsRepository,
                    ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Create ram metric.");
            _ramMetricsRepository.Create(_mapper.Map<RamMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetric>> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics call.");
            return Ok(_mapper.Map<List<RamMetric>>(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("getall")]
        public ActionResult<IList<RamMetric>> GetAllRamMetrics()
        {
            _logger.LogInformation("Get all ram metrics.");
            return Ok(_mapper.Map<List<RamMetric>>(_ramMetricsRepository.GetAll()));
        }

        //Более не нужен
        [HttpDelete("delete/{id}")]
        public ActionResult<IList<RamMetric>> DeleteRamMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete ram metrics.");
            _ramMetricsRepository.Delete(id);
            return Ok();
        }

        //Более не нужен
        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<RamMetric>> GetByIdRamMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id ram metrics.");
            return Ok(_ramMetricsRepository.GetById(id));
        }

        //Более не нужен
        [HttpPut("update")]
        public ActionResult<IList<RamMetric>> UpdateRamMetrics([FromBody] RamMetric item)
        {
            _logger.LogInformation("Update ram metrics.");
            _ramMetricsRepository.Update(item);
            return Ok();
        }
    }
}
