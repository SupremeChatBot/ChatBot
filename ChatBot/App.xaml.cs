using ChatBot.MVVM.ViewModel;
using ChatBot_Repo.EventAggregator;
using ChatBot_Repo.Services;
using ChatBot_Repo.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace ChatBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<IGoogleGeminiService, GoogleGeminiService>();
                    services.AddTransient<ICreateNewConversationService, CreateNewConversationService>();
                    services.AddSingleton<IEventAggregator, EventAggregator>();
                    services.AddSingleton<NewConversationDetailsViewModel>();
                }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await AppHost.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }



}
