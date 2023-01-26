using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Events.EntryComment;
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

            QueueFactory.CreateBasicConsumer()
                        .EnsureExchange(SozlukConstants.VoteExchangeName)
                        .EnsureQueue(SozlukConstants.DeleteEntryVoteQueueName, SozlukConstants.VoteExchangeName)
                        .Receive<DeleteEntryVoteEvent>(vote =>
                        {
                      
                            voteService.DeleteEntryVote(Guid.Parse(vote.EntryId), vote.UserId).GetAwaiter().GetResult();
                            _logger.LogInformation($"Delete entry Received EntryId :{vote.EntryId} ");
                        })
                        .StartConsumer(SozlukConstants.DeleteEntryVoteQueueName);

            QueueFactory.CreateBasicConsumer()
                        .EnsureExchange(SozlukConstants.VoteExchangeName)
                        .EnsureQueue(SozlukConstants.CreateEntryCommentVoteQueueName, SozlukConstants.VoteExchangeName)
                        .Receive<CreateEntryCommentVoteEvent>(vote =>
                        {

                            voteService.CreateEntryCommentVote(vote).GetAwaiter().GetResult();
                            _logger.LogInformation($"Create entry comment Received EntryCommentId :{vote.EntryCommentId} ");
                        })
                        .StartConsumer(SozlukConstants.CreateEntryCommentVoteQueueName);

            QueueFactory.CreateBasicConsumer()
                        .EnsureExchange(SozlukConstants.VoteExchangeName)
                        .EnsureQueue(SozlukConstants.DeleteEntryCommentVoteQueueName, SozlukConstants.VoteExchangeName)
                        .Receive<DeleteEntryCommentVoteEvent>(vote =>
                        {

                            voteService.DeleteEntryCommentVote(Guid.Parse(vote.EntryCommentId), vote.UserId).GetAwaiter().GetResult();
                            _logger.LogInformation($"Delete entry comment Received EntryCommentId :{vote.EntryCommentId} ");
                        })
                        .StartConsumer(SozlukConstants.DeleteEntryCommentVoteQueueName);

        }
    }
}