namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface IBCNews : IBCComponent
    {
        void Create(News item);

        IEnumerable<News> GetNewsItems();

        IEnumerable<News> GetLastNewsItems(int count = 15);
    }
}
