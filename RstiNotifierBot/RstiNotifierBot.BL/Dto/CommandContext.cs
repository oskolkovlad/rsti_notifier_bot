namespace RstiNotifierBot.BL.Dto
{
    using RstiNotifierBot.Common.Model.Entities;

    public class CommandContext
    {
        public CommandContext(Chat chat)
        {
            Chat = chat;
        }

        public Chat Chat { get; }
    }
}
