using Dapper;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Data.SQLite;
using System.Reflection;

namespace MetricsManager.Services.Impl
{
    public class AgentRepository : IAgentRepository
    {
        private readonly IOptions<DataBaseOptions> _databaseOptions;
        private readonly UriTypeHandler _uriTypeHandler;

        public AgentRepository(IOptions<DataBaseOptions> databaseOptions, UriTypeHandler uriTypeHandler)
        {
            _databaseOptions = databaseOptions;
            _uriTypeHandler = uriTypeHandler;
        }

        public void Create(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO Agents(AgentId, AgentAddress, Enable) VALUES(@AgentId, @AgentAddress, @Enable)",
                new
                {
                    AgentId = item.AgentId,
                    AgentAddress = item.AgentAddress,
                    Enable = item.Enable,
                });
        }

        public IList<AgentInfo> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            IList<AgentInfo> list = connection.Query<AgentInfo>("SELECT AgentId, AgentAddress, Enable FROM Agents").ToList();
            return list;
        }

        public AgentInfo GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            AgentInfo agent = connection.QuerySingle<AgentInfo>("SELECT AgentId, AgentAddress, Enable FROM Agents WHERE AgentId = @AgentId",
            new { AgentId = id });

            return agent;
        }

        public void Enable(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE Agents SET Enable = @Enable WHERE AgentId = @AgentId; ",
                new
                {
                    AgentId = id,
                    Enable = true
                });
        }

        public void Disable(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE Agents SET Enable = @Enable WHERE AgentId = @AgentId; ",
                new
                {
                    AgentId = id,
                    Enable = false
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM Agents WHERE AgentId=@AgentId",
            new
            {
                AgentId = id
            });
        }
    }
}
