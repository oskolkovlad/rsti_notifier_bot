namespace RstiNotifierBot.Controllers.Commands
{
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class InfoCommand : ICommand
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IInlineMarkupHandler _inlineMarkupHandler;

        public InfoCommand(IMessageHandler messageHandler, IInlineMarkupHandler inlineMarkupHandler)
        {
            _messageHandler = messageHandler;
            _inlineMarkupHandler = inlineMarkupHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Info; } }

        public CommandResult Execute(CommandContext context)
        {
            var message = _messageHandler.GetContactInfoMessage();
            var inlineMarkup = _inlineMarkupHandler.GetInfoReplyMarkup();
            var post = new PostDto(message, inlineMarkup: inlineMarkup);

            return new PostCommandResult(post);
        }

        #endregion
    }
}
