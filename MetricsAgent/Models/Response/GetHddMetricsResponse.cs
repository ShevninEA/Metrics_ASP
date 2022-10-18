using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Response
{
    public class GetHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
