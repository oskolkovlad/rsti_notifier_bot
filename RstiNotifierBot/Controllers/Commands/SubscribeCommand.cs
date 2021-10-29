namespace RstiNotifierBot.Controllers.Commands
{
    using System.Threading.Tasks;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;

    internal class SubscribeCommand : ICommand
    {
        private readonly ISubscribtionHandler _subscribtionHandler;

        public SubscribeCommand(ISubscribtionHandler subscribtionHandler)
        {
            _subscribtionHandler = subscribtionHandler;
        }

        #region ICommand Members

        public string Type { get { return Commands.Subscribe; } }

        public async Task<CommandResult> Execute(long chatId)
        {
            await _subscribtionHandler.Subscribe(chatId);

            return new CommandResult(true);
        }

        #endregion
    }
}
