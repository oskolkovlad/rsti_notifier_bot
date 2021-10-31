namespace RstiNotifierBot.Controllers.Commands
{
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class TopCommand : ICommand
    {
        private readonly IMessageHandler _messageHandler;

        public TopCommand(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Top5; } }

        public CommandResult Execute(CommandContext context)
        {
            var message = _messageHandler.GetTop5NewsMessage();
            var post = new PostDto(message);

            return new PostCommandResult(post);
        }

        #endregion
    }
}
