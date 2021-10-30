namespace RstiNotifierBot.Model.Entities
{
    internal class Chat
    {
        public Chat(long chatId, string username, string firstName, string lastName)
        {
            ChatId = chatId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }

        public long ChatId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
