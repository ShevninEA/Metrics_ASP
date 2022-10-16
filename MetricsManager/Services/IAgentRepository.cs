using MetricsManager.Models;

namespace MetricsManager.Services
{
    public interface IAgentRepository : IRepository<AgentInfo>
    {
        void Update(AgentInfo item);
    }
}
