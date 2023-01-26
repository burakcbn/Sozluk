using Sozluk.Common;
using Sozluk.Common.Events.User;
using Sozluk.Common.Infrastructure;
using Sozluk.Projections.UserService.Services;

namespace Sozluk.Projections.UserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Services.UserService _userService;
        private readonly EmailService _emailService;

        public Worker(ILogger<Worker> logger, UserService.Services.UserService userService, EmailService emailService)
        {
            _logger = logger;
            this._userService = userService;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            QueueFactory.CreateBasicConsumer()
                        .EnsureExchange(SozlukConstants.UserExchangeName)
                        .EnsureQueue(SozlukConstants.UserEmailChangedQueueName, SozlukConstants.UserExchangeName)
                        .Receive<UserEmailChangeEvent>(user =>
                        {
                            var confirmationId = _userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();

                            var link= _emailService.GenerateConfirmationLink(confirmationId);
                            _emailService.SendEmail(user.NewEmailAddress, link).GetAwaiter().GetResult();
                        })
                        .StartConsumer(SozlukConstants.CreateEntryVoteQueueName);
        }
    }
}