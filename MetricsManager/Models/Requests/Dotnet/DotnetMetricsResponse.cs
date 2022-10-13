using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests.Cpu
{
    public class DotnetMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public DotnetMetric[] Metrics { get; set; }
    }
}
