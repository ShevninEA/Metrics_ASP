using MetricsManager.Models;
using MetricsManager.Services.Client.Impl;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Polly;
using Microsoft.AspNetCore.HttpLogging;
using NLog.Web;
using FluentMigrator.Runner;
using MetricsManager.Services;
using MetricsManager.Services.Impl;

namespace MetricsManager
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSingleton<AgentPool>();

            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IAgentRepository, AgentRepository>();


            #region Configure Options

            builder.Services.Configure<DataBaseOptions>(options =>
            {
                builder.Configuration.GetSection("Settings:DataBaseOptions").Bind(options);
            });

            #endregion

            #region Configure Database

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                rb.AddSQLite()
                .WithGlobalConnectionString(builder.Configuration["Settings:DatabaseOptions:ConnectionString"].ToString())
                .ScanIn(typeof(Program).Assembly).For.Migrations()
                ).AddLogging(lb => lb.AddFluentMigratorConsole());

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

            //ConfigureSqlLiteConnection(builder);

            builder.Services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
                sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                onRetry: (response, sleepDuration, attemptCount, context) => {

                    var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                    logger.LogError(response.Exception != null ? response.Exception :
                        new Exception($"\n{response.Result.StatusCode}: {response.Result.RequestMessage}"),
                        $"(attempt: {attemptCount}) request exception.");
                }
                ));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });

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
    }
}