using MetricsManager.Models.Requests;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient<T,P>
    {
        T GetCpuMetrics(P request);
    }
}
