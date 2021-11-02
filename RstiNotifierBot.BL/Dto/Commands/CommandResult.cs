namespace RstiNotifierBot.BL.Dto.Commands
{
    public class CommandResult
    {
        public CommandResult() { }

        public CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }
}