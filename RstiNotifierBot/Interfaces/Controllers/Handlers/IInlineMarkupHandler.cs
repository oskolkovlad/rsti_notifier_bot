namespace RstiNotifierBot.Interfaces.Controllers.Handlers
{
    using RstiNotifierBot.Dto;

    internal interface IInlineMarkupHandler : IHandler
    {
        InlineButtonDto[][] GetPostReplyMarkup(string url);

        InlineButtonDto[][] GetInfoReplyMarkup();
    }
}
