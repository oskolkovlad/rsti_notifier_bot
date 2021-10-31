namespace RstiNotifierBot.Controllers.Handlers
{
    using RstiNotifierBot.Dto;
    using RstiNotifierBot.Interfaces.Controllers.Handlers;
    using RstiNotifierBot.Properties;

    internal class InlineMarkupHandler : IInlineMarkupHandler
    {
        private const string LinkCaption = "Перейти";
        private const string AllNewsCaption = "Все новости";
        private const string InstagramCaption = "Инстаграм";

        #region IInlineMarkupHandler Members

        public InlineButtonDto[][] GetPostReplyMarkup(string url)
        {
            var inlineMarkup = new InlineButtonDto[][]
            {
                new[] { new InlineButtonDto(LinkCaption, url) }
            };
            return inlineMarkup;
        }
        
        public InlineButtonDto[][] GetInfoReplyMarkup()
        {
            var inlineMarkup = new InlineButtonDto[][]
            {
                new[]
                {
                    new InlineButtonDto(AllNewsCaption, Resources.NewsUrl),
                    new InlineButtonDto(InstagramCaption, Resources.InstagramUrl)
                }
            };
            return inlineMarkup;
        }

        #endregion
    }
}
