namespace RstiNotifierBot.Dto
{
    using Telegram.Bot.Types;

    internal class CommandContext
    {
        public CommandContext(Chat chat)
        {
            Chat = chat;
        }

        public Chat Chat { get; private set; }
    }
}
