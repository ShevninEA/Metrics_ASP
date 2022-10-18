using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests.Cpu
{
    public class HddMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public HddMetric[] Metrics { get; set; }
    }
}
