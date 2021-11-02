namespace RstiNotifierBot.BL.Controllers.Commands
{
    using RstiNotifierBot.BL.Constants;
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;
    using RstiNotifierBot.BL.Interfaces.Commands;
    using RstiNotifierBot.BL.Interfaces.Handlers;

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
            var message = _subscriptionHandler.Unsubscribe(context.Chat.ChatId);
            var post = new PostDto(message);

            return new PostCommandResult(post);
        }

        #endregion
    }
}
