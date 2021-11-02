namespace RstiNotifierBot
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using RstiNotifierBot.Common.BL.Controllers;
    using RstiNotifierBot.Common.BL.Extensions;
    using RstiNotifierBot.Common.BL.Services;
    using RstiNotifierBot.Infrastructure.BC;
    using RstiNotifierBot.Infrastructure.BL;
    using RstiNotifierBot.Infrastructure.Model;

    internal class Program
    {
        private static async Task Main()
        {
            try
            {
                InitializeDependencies();
                await StartWorkAsync();
            }
            catch (Exception exception)
            {
                exception.OutputConsoleLog();
                return;
            }
            
            Thread.Sleep(Timeout.Infinite);
        }

        private static void InitializeDependencies()
        {
            ModelContainer.Instance.ConfigureContainer();
            BCContainer.Instance.ConfigureContainer();
            BLContainer.Instance.ConfigureContainer();
        }

        private static async Task StartWorkAsync()
        {
            var botManager = BLContainer.Instance.Get<ITelegramBotManager>();
            var newsTrackingService = BLContainer.Instance.Get<INewsTrackingService>();

            await botManager.StartAsync();
            newsTrackingService.Start();
        }
    }
}
