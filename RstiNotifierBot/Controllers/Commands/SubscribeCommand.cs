namespace RstiNotifierBot.Controllers.Commands
{
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class SubscribeCommand : ICommand
    {
        private readonly ISubscriptionHandler _subscriptionHandler;

        public SubscribeCommand(ISubscriptionHandler subscriptionHandler)
        {
            _subscriptionHandler = subscriptionHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Subscribe; } }

        public CommandResult Execute(CommandContext context)
        {
            var message = _subscriptionHandler.Subscribe(context.Chat.Id);
            return new PostCommandResult(message);
        }

        #endregion
    }
}
