namespace RstiNotifierBot.BL.Controllers.Commands
{
    using RstiNotifierBot.BL.Constants;
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;
    using RstiNotifierBot.BL.Interfaces.Commands;
    using RstiNotifierBot.BL.Interfaces.Handlers;

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
            var message = _subscriptionHandler.Subscribe(context.Chat.ChatId);
            var post = new PostDto(message);

            return new PostCommandResult(post);
        }

        #endregion
    }
}
