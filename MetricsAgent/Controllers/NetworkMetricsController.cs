﻿using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Services.Impl;
using System.Collections.Generic;
using MetricsAgent.Models.Dto;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository networkMetricsRepository,
                    ILogger<NetworkMetricsController> logger, IMapper mapper)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation("Create network metric.");
            _networkMetricsRepository.Create(_mapper.Map<NetworkMetric>(request));
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetric>> GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get network metrics call.");
            return Ok(_mapper.Map <List<NetworkMetricDto>>(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("getall")]
        public ActionResult<IList<NetworkMetric>> GetAllNetworkMetrics()
        {
            _logger.LogInformation("Get all network metrics.");
            return Ok(_mapper.Map<List<NetworkMetricDto>>(_networkMetricsRepository.GetAll()));
        }

        //Более не нужен
        [HttpDelete("delete/{id}")]
        public ActionResult<IList<NetworkMetric>> DeleteNetworkMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Delete network metrics.");
            _networkMetricsRepository.Delete(id);
            return Ok();
        }

        //Более не нужен
        [HttpGet("getbyid/{id}")]
        public ActionResult<IList<NetworkMetric>> GetByIdNetworkMetrics([FromRoute] int id)
        {
            _logger.LogInformation("Get by id network metrics.");
            return Ok(_networkMetricsRepository.GetById(id));
        }

        //Более не нужен
        [HttpPut("update")]
        public ActionResult<IList<NetworkMetric>> UpdateNetworkMetrics([FromBody] NetworkMetric item)
        {
            _logger.LogInformation("Update network metrics.");
            _networkMetricsRepository.Update(item);
            return Ok();
        }
    }
}

