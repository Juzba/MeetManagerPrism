using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MeetManagerPrism.Services
{
    public class AppBootstraper : PrismApplication
    {
        public static IHost AppHost { get; private set; }


        public AppBootstraper()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            {
                services.AddTransient<MainWindow>();



            }).Build();
        }





        protected override Window CreateShell()
        {
            return AppHost.Services.GetRequiredService<MainWindow>();
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();

            var mainVindow = CreateShell();
            mainVindow.Show();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();


            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            AppHost.Dispose();

            base.OnExit(e);
        }






    }
}
