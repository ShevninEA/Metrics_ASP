using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Response
{
    public class GetCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
