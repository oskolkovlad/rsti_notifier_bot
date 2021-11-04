namespace RstiNotifierBot.BL.Controllers.Commands
{
    using RstiNotifierBot.BL.Constants;
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;
    using RstiNotifierBot.BL.Interfaces.Commands;
    using RstiNotifierBot.Common.BC;

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
            var chat = context.Chat;
            if (!_bcChat.IsExists(chat.ChatId))
            {
                _bcChat.Create(chat);
            }

            return new CommandResult(true);
        }

        #endregion
    }
}
