namespace RstiNotifierBot.Controllers.Commands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RstiNotifierBot.BusinessObjects.Constants;
    using RstiNotifierBot.Dto.Commands;
    using RstiNotifierBot.Interfaces.Controllers.Commands;

    internal class CommandsInvoker : ICommandsInvoker
    {
        private readonly IDictionary<string, ICommand> _commands;

        public CommandsInvoker(params ICommand[] commands)
        {
            _commands = new Dictionary<string, ICommand>();

            foreach (var command in commands)
            {
                _commands.Add(command.Type, command);
            }
        }

        #region ICommandsInvoker Members

        public async Task<CommandResult> Execute(long chatId, string command)
        {
            CommandResult result;

            switch (command)
            {
                case Commands.Last:
                case Commands.Top5:
                case Commands.Subscribe:
                case Commands.Unsubscribe:
                case Commands.Info:
                    result = await _commands[command].Execute(chatId);
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }

        #endregion
    }
}
