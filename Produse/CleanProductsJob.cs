using ProductWebAPI.Models;

namespace ProductWebAPI
{
    public class CleanProductsJob : BackgroundService
    {
        private readonly ILogger<CleanProductsJob> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CleanProductsJob(ILogger<CleanProductsJob> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background job starting");
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background job started");
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Background job running at {time}", DateTime.UtcNow);

                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ProductContext>();

                    var emptyProducts = db.ProductItems.Where(p => p.Stock == 0);
                    db.ProductItems.RemoveRange(emptyProducts);
                    await db.SaveChangesAsync();

                    _logger.LogInformation("Products with 0 stock removed");
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Background job stopping");
            }
            finally
            {
                _logger.LogInformation("Worker stopping");
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background job stopping");
            await base.StopAsync(cancellationToken);
        }
    }
}
