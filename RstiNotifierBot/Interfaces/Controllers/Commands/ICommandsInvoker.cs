namespace RstiNotifierBot.Interfaces.Controllers.Commands
{
    using System.Threading.Tasks;
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;

    internal interface ICommandsInvoker
    {
        Task<CommandResult> Execute(CommandContext context, string command);
    }
}