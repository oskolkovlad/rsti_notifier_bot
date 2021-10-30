namespace RstiNotifierBot.Interfaces.Controllers.Commands
{
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;

    internal interface ICommandsInvoker
    {
        CommandResult Execute(CommandContext context, string command);
    }
}