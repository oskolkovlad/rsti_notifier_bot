namespace RstiNotifierBot.Controllers.Commands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class LastCommand : ICommand
    {
        private const string LinkCaption = "Перейти";

        private readonly IMessageHandler _messageHandler;

        public LastCommand(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Last; } }

        public async Task<CommandResult> Execute(long chatId)
        {
            var (message, url, imageUrl) = await _messageHandler.GetLastNewsMessage();
            var inlineMarkup = new List<List<InlineButtonDto>>
            {
                new List<InlineButtonDto> { new InlineButtonDto(LinkCaption, url) }
            };

            return new PostCommandResult(message, imageUrl, inlineMarkup);
        }

        #endregion
    }
}
