using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Response
{
    public class GetRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
