namespace RstiNotifierBot.Dto.Commands
{
    using System.Collections.Generic;

    internal class PostCommandResult : CommandResult
    {
        public PostCommandResult(string message, string imageUrl = null,
            IEnumerable<IEnumerable<InlineButtonDto>> inlineMarkup = null)
            : base(true)
        {
            Message = message;
            ImageUrl = imageUrl;
            InlineMarkup = inlineMarkup;
        }

        public string Message { get; private set; }

        public string ImageUrl { get; private set; }

        public IEnumerable<IEnumerable<InlineButtonDto>> InlineMarkup { get; private set; }
    }
}