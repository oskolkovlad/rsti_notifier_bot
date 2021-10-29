namespace RstiNotifierBot.Controllers.Commands
{
    using System.Threading.Tasks;
    using RstiNotifierBot.BusinessObjects.Constants;
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

        public async Task<CommandResult> Execute(long chatId)
        {
            var message = await _messageHandler.GetTop5NewsMessage();
            return new PostCommandResult(message);
        }

        #endregion
    }
}
