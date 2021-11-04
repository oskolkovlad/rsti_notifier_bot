namespace RstiNotifierBot.Common.BL.Controllers
{
    using System.Threading.Tasks;

    public interface ITelegramBotManager : IComponent
    {
        Task StartAsync();

        Task StopAsync();
    }
}
