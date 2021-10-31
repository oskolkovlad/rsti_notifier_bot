namespace RstiNotifierBot.Model.Entities
{
    internal class Chat
    {
        public Chat() { }

        public Chat(long chatId, string username, string firstName, string lastName, string title, string type)
        {
            ChatId = chatId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            Type = type;
        }

        public long ChatId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }
    }
}
