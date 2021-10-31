namespace RstiNotifierBot.Controllers.Commands
{
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.BusinessComponents;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Model.Entities;

    internal class StartCommand : ICommand
    {
        private readonly IBCChat _bcChat;

        public StartCommand(IBCChat bcChat)
        {
            _bcChat = bcChat;
        }

        #region ICommand Members

        public string Type { get { return Commands.Start; } }

        public CommandResult Execute(CommandContext context)
        {
            var chatInfo = context.Chat;
            var chat = new Chat(chatInfo.Id, chatInfo.Username, chatInfo.FirstName, chatInfo.LastName,
                chatInfo.Title, chatInfo.Type.ToString());

            if (!_bcChat.IsExists(chat.ChatId))
            {
                _bcChat.Create(chat);
            }

            return new CommandResult(true);
        }

        #endregion
    }
}
