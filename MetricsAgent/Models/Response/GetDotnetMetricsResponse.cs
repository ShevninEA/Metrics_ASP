using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Response
{
    public class GetDotnetMetricsResponse
    {
        public List<DotnetMetricDto> Metrics { get; set; }
    }
}
