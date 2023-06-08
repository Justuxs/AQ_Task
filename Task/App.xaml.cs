using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Task.Data;

namespace Task
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AllocConsole();


            var services = new ServiceCollection();

            services.AddSingleton<DatabaseManager>();
            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var databaseManager = _serviceProvider.GetRequiredService<DatabaseManager>();

            mainWindow.databaseManager = databaseManager;
            databaseManager.DropDatabase();
            databaseManager.CreateDatabase();
            databaseManager.InsertData();

            mainWindow.Show();
        }
    }
}
