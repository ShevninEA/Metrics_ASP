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

            return connection.QuerySingle<NetworkMetrics>("SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
                new { id = id });
        }

        public IList<NetworkMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на получение всех данных за период из таблицы
            cmd.CommandText = "SELECT * FROM networkmetrics where time >= @timeFrom and time <= @timeTo";
            cmd.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds);
            cmd.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds);
            var returnList = new List<NetworkMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // Пока есть что читать — читаем
                while (reader.Read())
                {
                    // Добавляем объект в список возврата
                    returnList.Add(new NetworkMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return returnList;
        }

        public void Update(NetworkMetrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на обновление данных
            cmd.CommandText = "UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id; ";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
