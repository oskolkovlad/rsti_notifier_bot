namespace RstiNotifierBot.Controllers.Commands
{
    using System.Threading.Tasks;
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

        public async Task<CommandResult> Execute(CommandContext context)
        {
            var chatInfo = context.Chat;
            var chat = new Chat
            {
                ChatId = chatInfo.Id,
                Username = chatInfo.Username,
                FirstName = chatInfo.FirstName,
                LastName = chatInfo.LastName
            };
            _bcChat.CreateChat(chat);

            return new CommandResult(true);
        }

        #endregion
    }
}
