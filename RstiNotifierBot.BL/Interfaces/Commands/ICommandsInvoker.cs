namespace RstiNotifierBot.BL.Interfaces.Commands
{
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;

    internal interface ICommandsInvoker
    {
        CommandResult Execute(CommandContext context, string command);
    }
}