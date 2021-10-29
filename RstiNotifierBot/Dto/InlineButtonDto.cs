namespace RstiNotifierBot.Dto
{
    internal class InlineButtonDto
    {
        public InlineButtonDto(string text, string url = null, string callbackData = null)
        {
            Text = text;
            Url = url;
            CallbackData = callbackData;
        }

        public string Text { get; private set; }

        public string Url { get; private set; }

        public string CallbackData { get; private set; }
    }
}
