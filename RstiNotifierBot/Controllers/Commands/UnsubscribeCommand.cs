namespace RstiNotifierBot.Controllers.Commands
{
    using System.Threading.Tasks;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class UnsubscribeCommand : ICommand
    {
        private readonly ISubscribtionHandler _subscribtionHandler;

        public UnsubscribeCommand(ISubscribtionHandler subscribtionHandler)
        {
            _subscribtionHandler = subscribtionHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Unsubscribe; } }

        public async Task<CommandResult> Execute(CommandContext context)
        {
            var message = await _subscribtionHandler.Unsubscribe(context.Chat.Id);

            return new PostCommandResult(message);
        }

        #endregion
    }
}
