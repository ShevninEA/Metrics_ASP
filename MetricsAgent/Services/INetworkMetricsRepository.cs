using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics>
    {
        IList<NetworkMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
