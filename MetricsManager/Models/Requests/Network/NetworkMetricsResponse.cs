using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests.Cpu
{
    public class NetworkMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public NetworkMetric[] Metrics { get; set; }
    }
}
