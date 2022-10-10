using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.Job;
using MetricsAgent.Jobs;
using MetricsAgent.Mapping;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ConfigureSqlLiteConnection();

            var builder = WebApplication.CreateBuilder(args);

            #region Configure Options

            builder.Services.Configure<DataBaseOptions>(options =>
            {
                builder.Configuration.GetSection("Settings:DataBaseOptions").Bind(options);
            });

            #endregion

            #region Configure Mapping

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new
                MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);


            #endregion

            #region Configure Database

            //ConfigureSqlLiteConnection(builder);

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                rb.AddSQLite()
                .WithGlobalConnectionString(builder.Configuration["Settings:DatabaseOptions:ConnectionString"].ToString())
                .ScanIn(typeof(Program).Assembly).For.Migrations()
                ).AddLogging(lb => lb.AddFluentMigratorConsole());

            #endregion

            #region Quartz

            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();

            builder.Services.AddSingleton<CpuMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof (CpuMetricJob),
                "0/5 * * ? * * *"));

            builder.Services.AddSingleton<DotnetMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(DotnetMetricJob),
                "0/5 * * ? * * *"));

            builder.Services.AddSingleton<HddMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(HddMetricJob),
                "0/5 * * ? * * *"));

            builder.Services.AddSingleton<NetworkMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(NetworkMetricJob),
                "0/5 * * ? * * *"));

            builder.Services.AddSingleton<RamMetricJob>();
            builder.Services.AddSingleton(new JobSchedule(typeof(RamMetricJob),
                "0/5 * * ? * * *"));

            builder.Services.AddHostedService<QuartzHostedService>();

            #endregion

            #region Configure logging

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            #endregion

            // Add services to the container.

            builder.Services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            builder.Services.AddScoped<IDotnetMetricsRepository, DotnetMetricsRepository>();
            builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

                // Поддержка TimeSpan
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
            {
                var migrationRunner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationRunner.MigrateUp();
            }

            app.Run();
        }

        //private static void ConfigureSqlLiteConnection()
        //{
        //    const string connectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        //    var connection = new SQLiteConnection(connectionString);
        //    connection.Open();
        //    PrepareSchema(connection);
        //}

        //private static void PrepareSchema(SQLiteConnection connection)
        //{
        //    using (var command = new SQLiteCommand(connection))
        //    {
        //        //Задаём новый текст команды для выполнения
        //        //// Удаляем таблицу с метриками, если она есть в базе данных
        //        //command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
        //        // Отправляем запрос в базу данных
        //        //command.ExecuteNonQuery();
        //        command.CommandText =
        //            @"CREATE TABLE rammetrics(id INTEGER
        //            PRIMARY KEY,
        //            value INT, time INT)";
        //        command.ExecuteNonQuery();
        //    }
        //}
    }
}