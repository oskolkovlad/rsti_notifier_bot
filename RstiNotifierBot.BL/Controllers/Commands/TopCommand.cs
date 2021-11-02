namespace RstiNotifierBot.BL.Controllers.Commands
{
    using RstiNotifierBot.BL.Constants;
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;
    using RstiNotifierBot.BL.Interfaces.Commands;
    using RstiNotifierBot.BL.Interfaces.Handlers;

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
