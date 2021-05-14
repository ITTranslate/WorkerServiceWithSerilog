using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyService
{
    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<Worker> _logger;

        public Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("上班了，又是精神抖擞的一天，output from StartAsync");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                // 这里实现实际的业务逻辑
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                        await SomeMethodThatDoesTheWork(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Global exception occurred. Will resume in a moment.");
                    }

                    await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
                }
            }
            finally
            {
                _logger.LogCritical("Exiting application...");
                GetOffWork(stoppingToken);
                _hostApplicationLifetime.StopApplication();
            }
        }

        private async Task SomeMethodThatDoesTheWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("我爱工作，埋头苦干ing……");
            await Task.CompletedTask;
        }

        /// <summary>
        /// 关闭前需要完成的工作
        /// </summary>
        private void GetOffWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("我爱工作，我要加班……");

            _logger.LogInformation("不行，我爱加班，我要再干 20 秒，Wait 1 ");

            Task.Delay(TimeSpan.FromSeconds(20)).Wait();

            _logger.LogInformation("不行，我爱加班，我要再干 1 分钟，Wait 2 ");

            Task.Delay(TimeSpan.FromMinutes(1)).Wait();

            _logger.LogInformation("顶不住了，下班走人");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("下班时间到了，output from StopAsync ");
            return base.StopAsync(cancellationToken);
        }
    }
}
