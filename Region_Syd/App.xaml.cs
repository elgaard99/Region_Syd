using System.Windows;
using Microsoft.Extensions.Configuration;

namespace Region_Syd
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfigurationRoot Configuration { get; private set; }

        public App()
        {

            var builder = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

        }
    }
}
