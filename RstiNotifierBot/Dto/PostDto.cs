namespace RstiNotifierBot.Dto
{
    internal class PostDto
    {
        public PostDto(string message, string imageUrl = null, InlineButtonDto[][] inlineMarkup = null)
        {
            Message = message;
            ImageUrl = imageUrl;
            InlineMarkup = inlineMarkup;
        }

        public string Message { get; private set; }

        public string ImageUrl { get; private set; }

        public InlineButtonDto[][] InlineMarkup { get; private set; }
    }
}
