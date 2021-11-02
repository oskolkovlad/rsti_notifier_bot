namespace RstiNotifierBot.BL.Dto
{
    public class InlineButtonDto
    {
        public InlineButtonDto(string text, string url = null, string callbackData = null)
        {
            Text = text;
            Url = url;
            CallbackData = callbackData;
        }

        public string Text { get; }

        public string Url { get; }

        public string CallbackData { get; }
    }
}
