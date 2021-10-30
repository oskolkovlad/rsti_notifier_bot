namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using RstiNotifierBot.Dto;

    internal interface IBCNewsProperty
    {
        NewsDto GetLastNewsItem();
    }
}
