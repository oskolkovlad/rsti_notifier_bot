namespace RstiNotifierBot.Interfaces.Controllers.Commands
{
    using System.Threading.Tasks;
    using RstiNotifierBot.Dto.Commands;

    internal interface ICommand
    {
        string Type { get; }

        Task<CommandResult> Execute(long chatId);
    }
}
