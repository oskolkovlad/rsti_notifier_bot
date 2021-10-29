namespace RstiNotifierBot.Dto.Commands
{
    internal class CommandResult
    {
        public CommandResult() { }

        public CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; private set; }
    }
}