using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Impl
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IOptions<DataBaseOptions> _databaseOptions;

        public NetworkMetricsRepository(IOptions<DataBaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(NetworkMetrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new { value = item.Value, time = item.Time });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM networkmetrics WHERE id=@id",
                new { id = id });
        }

        public IList<NetworkMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<NetworkMetrics>("SELECT Id, Time, Value FROM networkmetrics").ToList();
        }

        public NetworkMetrics GetById(int id)
        {

            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.QuerySingle<NetworkMetrics>("SELECT Id, Time, Value FROM hddmetrics WHERE id = @id",
                new { id = id });
        }

        public IList<NetworkMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<NetworkMetrics>("SELECT * FROM networkmetrics where time >= @timeFrom and time <= @timeTo",
                new { timeFrom = timeFrom.TotalSeconds, timeTo = timeTo.TotalSeconds }).ToList();
        }

        public void Update(NetworkMetrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id;",
                new { value = item.Value, time = item.Time, id = item.Id });
        }
    }
}
