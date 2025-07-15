/*

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Windows;

namespace MeetManagerPrism.Services
{
    public class AppBootstraper : PrismApplication
    {
        public static IHost AppHost { get; private set; } = null!;


        public AppBootstraper()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            {
                services.AddTransient<MainWindow>();
            }).Build();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) { }


        protected override Window CreateShell()
        {
            return AppHost.Services.GetRequiredService<MainWindow>();
        }

        protected override async void OnInitialized()
        {
            base.OnInitialized();
       
            await AppHost.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            AppHost.Dispose();

            base.OnExit(e);
        }

    }
}
*/