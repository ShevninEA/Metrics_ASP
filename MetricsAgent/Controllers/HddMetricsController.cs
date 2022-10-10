using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.Dto;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd/left")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly IMapper _mapper;

        public HddMetricsController(IHddMetricsRepository hddMetricsRepository,
                    ILogger<HddMetricsController> logger, IMapper mapper)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        ////Более не нужен
        //[HttpPost("create")]
        //public IActionResult Create([FromBody] HddMetricCreateRequest request)
        //{
        //    _logger.LogInformation("Create hdd metric.");
        //    _hddMetricsRepository.Create(_mapper.Map<HddMetric>(request));
        //    return Ok();
        //}

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<HddMetricDto>> GetHddMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get hdd metrics call.");
            return Ok(_mapper.Map<List<HddMetricDto>>(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("getall")]
        public ActionResult<IList<HddMetricDto>> GetAllHddMetrics()
        {
            _logger.LogInformation("Get all hdd metrics.");
            return Ok(_mapper.Map<List<HddMetricDto>>(_hddMetricsRepository.GetAll()));
        }

        ////Более не нужен
        //[HttpDelete("delete/{id}")]
        //public ActionResult<IList<HddMetricDto>> DeleteHddMetrics([FromRoute] int id)
        //{
        //    _logger.LogInformation("Delete hdd metrics.");
        //    _hddMetricsRepository.Delete(id);
        //    return Ok();
        //}

        ////Более не нужен
        //[HttpGet("getbyid/{id}")]
        //public ActionResult<IList<HddMetricDto>> GetByIdHddMetrics([FromRoute] int id)
        //{
        //    _logger.LogInformation("Get by id hdd metrics.");
        //    return Ok(_hddMetricsRepository.GetById(id));
        //}

        ////Более не нужен
        //[HttpPut("update")]
        //public ActionResult<IList<HddMetricDto>> UpdateHddMetrics([FromBody] HddMetric item)
        //{
        //    _logger.LogInformation("Update hdd metrics.");
        //    _hddMetricsRepository.Update(item);
        //    return Ok();
        //}
    }
}
