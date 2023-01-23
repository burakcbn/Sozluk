using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Projections.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var constr = _configuration["ConnectionStrings"]; 
            var favService = new Services.FavoriteService(constr);

            QueueFactory.CreateBasicConsumer()
                        .EnsureExchange(SozlukConstants.FavExchangeName)
                        .EnsureQueue(SozlukConstants.CreateEntryFavQueueName, SozlukConstants.FavExchangeName)
                        .Receive<CreateEntryFavEvent>(fav =>
                        {
                            favService.CreateEntryFav(fav).GetAwaiter().GetResult();
                            _logger.LogInformation($"Received EntryId {fav.EntryId}");
                        })
                        .StartConsumer(SozlukConstants.CreateEntryFavQueueName);
                        
        }
    }
}