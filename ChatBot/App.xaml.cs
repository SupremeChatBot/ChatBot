using ChatBot.MVVM.ViewModel;
using ChatBot_Repo.Services;
using ChatBot_Repo.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public IConfiguration Configuration { get; private set; }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGoogleGeminiService, GoogleGeminiService>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create a service collection and configure services
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build the service provider
            //var serviceProvider = serviceCollection.BuildServiceProvider();

            //// Create the main window and set its data context to the view model
            //var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            //mainWindow.DataContext = serviceProvider.GetRequiredService<MainViewModel>();

            //// Show the main window
            //mainWindow.Show();
        }

    }



}
