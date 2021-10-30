namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using RstiNotifierBot.Model.Entities;

    internal interface IBCNews
    {
        News GetLastNewsItem();
    }
}
