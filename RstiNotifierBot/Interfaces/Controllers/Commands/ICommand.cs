namespace RstiNotifierBot.Interfaces.Controllers.Commands
{
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Dto.Commands;

    internal interface ICommand
    {
        string Type { get; }

        CommandResult Execute(CommandContext context);
    }
}
