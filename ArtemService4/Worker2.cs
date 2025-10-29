using Microsoft.Extensions.Options;
using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtemService4
{
    public class Worker2 : BackgroundService
    {
        private readonly ILogger<Worker2> _logger;
        private readonly WorkerSettings _settings;
        public Worker2(ILogger<Worker2> logger, IOptions<WorkerSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker2 running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation($"{_settings.Worker2}");

                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

