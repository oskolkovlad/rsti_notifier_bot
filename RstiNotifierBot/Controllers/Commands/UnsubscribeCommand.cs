namespace RstiNotifierBot.Controllers.Commands
{
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class UnsubscribeCommand : ICommand
    {
        private readonly ISubscriptionHandler _subscriptionHandler;

        public UnsubscribeCommand(ISubscriptionHandler subscriptionHandler)
        {
            _subscriptionHandler = subscriptionHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Unsubscribe; } }

        public CommandResult Execute(CommandContext context)
        {
            var message = _subscriptionHandler.Unsubscribe(context.Chat.Id);
            var post = new PostDto(message);

            return new PostCommandResult(post);
        }

        #endregion
    }
}
