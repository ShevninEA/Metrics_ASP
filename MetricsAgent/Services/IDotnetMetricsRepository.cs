using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IDotnetMetricsRepository : IRepository<DotnetMetrics>
    {
        IList<DotnetMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
