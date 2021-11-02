namespace RstiNotifierBot.Common.BC
{
    using System.Collections.Generic;
    using RstiNotifierBot.Common.Model.Entities;

    public interface IBCNews : IBCComponent
    {
        void Create(News item);

        IEnumerable<News> GetNewsItems();

        IEnumerable<News> GetLastNewsItems(int count = 15);
    }
}
