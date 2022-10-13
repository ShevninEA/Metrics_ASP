namespace MetricsManager.Models.Requests.Cpu
{
    public class HddMetricsRequest
    {
        public int AgentId { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
