namespace RstiNotifierBot.BL.Interfaces.Commands
{
    using RstiNotifierBot.BL.Dto;
    using RstiNotifierBot.BL.Dto.Commands;

    internal interface ICommand
    {
        string Type { get; }

        CommandResult Execute(CommandContext context);
    }
}
