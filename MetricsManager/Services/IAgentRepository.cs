﻿using MetricsManager.Models;

namespace MetricsManager.Services
{
    public interface IAgentRepository
    {
        IList<AgentInfo> GetAll();
        AgentInfo GetById(int id);
        void Create(AgentInfo item);
        void Update(AgentInfo item);
        void Delete(int item);
    }
}
