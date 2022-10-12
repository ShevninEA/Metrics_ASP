using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class CpuMetric
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("Value")]
        public int Value { get; set; }
    }
}
