using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Projections.VoteService
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
            var conStr = _configuration["ConnectionStrings"];
            var voteService = new Services.VoteService(conStr);

            QueueFactory.CreateBasicConsumer()
                        .EnsureExchange(SozlukConstants.VoteExchangeName)
                        .EnsureQueue(SozlukConstants.CreateEntryVoteQueueName, SozlukConstants.VoteExchangeName)
                        .Receive<CreateEntryVoteEvent>(vote =>
                        {

                            voteService.CreateEntryVote(vote).GetAwaiter().GetResult();
                            _logger.LogInformation($"Create entry Received EntryId :{vote.EntryId} VoteType:{vote.VoteType}");
                        })
                        .StartConsumer(SozlukConstants.CreateEntryVoteQueueName);

        }
    }
}