namespace RstiNotifierBot.Interfaces.Model.Repositories
{
    using RstiNotifierBot.Model.Entities;

    internal interface IChatPropertyRepository
    {
        void Create(ChatProperty chatProperty);

        ChatProperty GetProperty(long chatId, string name, string value);

        void Delete(long chatId, string name);
    }
}
