namespace RstiNotifierBot.BL.Interfaces.Handlers
{
    using RstiNotifierBot.BL.Dto;

    internal interface IInlineMarkupHandler : IHandler
    {
        InlineButtonDto[][] GetPostReplyMarkup(string url);

        InlineButtonDto[][] GetInfoReplyMarkup();
    }
}
