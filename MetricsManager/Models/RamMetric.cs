using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class RamMetric
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("Value")]
        public int Value { get; set; }
    }
}
