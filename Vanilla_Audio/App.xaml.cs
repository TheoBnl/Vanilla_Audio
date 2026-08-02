using Business.Data;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using ViewModels;

namespace Vanilla_Audio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost host;

        protected override void OnStartup(StartupEventArgs e)
        {
            this.host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IFolderPathManager>(sp => new FolderPathManager("folderPath.json"));
                    services.AddSingleton<ITagManager, TagLibSharpManager>();
                    services.AddSingleton<ISongManager, SongManager>();
                    services.AddSingleton<MainWindowVM>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();

            this.host.Start();

            MainWindow mainWindow = this.host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.host.Dispose();
            base.OnExit(e);
        }
    }

}
