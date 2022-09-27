using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {
        IList<RamMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
