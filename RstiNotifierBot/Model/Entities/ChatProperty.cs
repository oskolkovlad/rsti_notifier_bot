namespace RstiNotifierBot.Model.Entities
{
    internal class ChatProperty
    {
        public ChatProperty() { }

        public ChatProperty(string chatPropertyId, long chatId, string name, string value)
        {
            ChatPropertyId = chatPropertyId;
            ChatId = chatId;
            Name = name;
            Value = value;
        }

        public string ChatPropertyId { get; set; }

        public long ChatId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
