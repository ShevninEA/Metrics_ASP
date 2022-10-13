using MetricsManager.Models.Requests.Cpu;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
        DotnetMetricsResponse GetDotnetMetrics(DotnetMetricsRequest request);
        HddMetricsResponse GetHddMetrics(HddMetricsRequest request);
        NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request);
        RamMetricsResponse GetRamMetrics(RamMetricsRequest request);
    }
}
