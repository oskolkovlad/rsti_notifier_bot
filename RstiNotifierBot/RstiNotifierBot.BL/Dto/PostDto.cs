namespace RstiNotifierBot.BL.Dto
{
    public class PostDto
    {
        public PostDto(string message, string imageUrl = null, InlineButtonDto[][] inlineMarkup = null)
        {
            Message = message;
            ImageUrl = imageUrl;
            InlineMarkup = inlineMarkup;
        }

        public string Message { get; }

        public string ImageUrl { get; }

        public InlineButtonDto[][] InlineMarkup { get; }
    }
}
