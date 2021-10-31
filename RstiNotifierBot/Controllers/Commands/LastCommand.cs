namespace RstiNotifierBot.Controllers.Commands
{
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class LastCommand : ICommand
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IInlineMarkupHandler _inlineMarkupHandler;

        public LastCommand(IMessageHandler messageHandler, IInlineMarkupHandler inlineMarkupHandler)
        {
            _messageHandler = messageHandler;
            _inlineMarkupHandler = inlineMarkupHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Last; } }

        public CommandResult Execute(CommandContext context)
        {
            var (message, url, imageUrl) = _messageHandler.GetLastNewsMessage();
            var inlineMarkup = _inlineMarkupHandler.GetPostReplyMarkup(url);
            var post = new PostDto(message, imageUrl, inlineMarkup);

            return new PostCommandResult(post);
        }

        #endregion
    }
}
