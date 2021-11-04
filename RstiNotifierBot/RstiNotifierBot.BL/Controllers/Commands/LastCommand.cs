namespace RstiNotifierBot.BL.Controllers.Commands
{
    using RstiNotifierBot.BL.Constants;
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;
    using RstiNotifierBot.BL.Interfaces.Commands;
    using RstiNotifierBot.BL.Interfaces.Handlers;

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
