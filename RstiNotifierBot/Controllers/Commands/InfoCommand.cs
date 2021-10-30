namespace RstiNotifierBot.Controllers.Commands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;
    using RstiNotifierBot.Properties;

    internal class InfoCommand : ICommand
    {
        private const string AllNewsCaption = "Все новости";
        private const string InstagramCaption = "Инстаграм";

        private readonly IMessageHandler _messageHandler;

        public InfoCommand(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Info; } }

        public async Task<CommandResult> Execute(CommandContext context)
        {
            var post = _messageHandler.GetContactInfoMessage();
            var inlineMarkup = new List<List<InlineButtonDto>>
            {
                new List<InlineButtonDto>
                {
                    new InlineButtonDto(AllNewsCaption, Resources.NewsUrl),
                    new InlineButtonDto(InstagramCaption, Resources.InstagramUrl)
                }
            };

            return new PostCommandResult(post, inlineMarkup: inlineMarkup);
        }

        #endregion
    }
}
