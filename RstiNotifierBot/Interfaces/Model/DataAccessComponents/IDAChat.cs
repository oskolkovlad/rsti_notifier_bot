namespace RstiNotifierBot.Interfaces.Model.DataAccessComponents
{
    using System.Collections.Generic;
    using RstiNotifierBot.Model.Entities;

    internal interface IDAChat
    {
        IEnumerable<Chat> GetSubscribtionChatIds();
    }
}
