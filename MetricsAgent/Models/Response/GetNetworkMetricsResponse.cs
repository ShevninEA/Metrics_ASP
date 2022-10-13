using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Response
{
    public class GetNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
